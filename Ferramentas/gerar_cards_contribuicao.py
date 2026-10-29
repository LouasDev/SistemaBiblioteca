#!/usr/bin/env python3
"""Gera cards SVG das contribuições da equipe a partir do histórico Git."""

from __future__ import annotations

import argparse
import base64
import html
import subprocess
from datetime import date
from urllib.request import Request, urlopen
from collections import defaultdict
from pathlib import Path


CONTRIBUTORS = {
    "renato0x": {"f0ckthepattern@gmail.com", "renaatomonteiro@gmail.com"},
    "LouasDev": {"louasdev@gmail.com", "louasmarinho@gmail.com"},
    "Ithaloluzdepanela": {
        "163846279+Ithaloluzdepanela@users.noreply.github.com",
        "ithal@aluno",
        "ithalop@gmail.com",
        "ithalop48@gmail.com",
    },
}

AVATAR_URLS = {
    "renato0x": "https://avatars.githubusercontent.com/u/199422215?v=4&s=80",
    "LouasDev": "https://avatars.githubusercontent.com/u/269842284?v=4&s=80",
    "Ithaloluzdepanela": "https://avatars.githubusercontent.com/u/163846279?v=4&s=80",
}


def load_avatar_data_uri(handle: str) -> str:
    """Baixa o avatar para que o SVG não dependa de imagens externas."""
    request = Request(AVATAR_URLS[handle], headers={"User-Agent": "SistemaBiblioteca-cards"})
    with urlopen(request, timeout=20) as response:
        content_type = response.headers.get_content_type()
        content = response.read()

    if not content_type.startswith("image/"):
        raise ValueError(f"O avatar de {handle} não retornou uma imagem.")

    encoded = base64.b64encode(content).decode("ascii")
    return f"data:{content_type};base64,{encoded}"


def collect_statistics(repository: Path) -> tuple[dict[str, dict[str, int]], dict[str, dict[str, int]]]:
    aliases = {email.lower(): handle for handle, emails in CONTRIBUTORS.items() for email in emails}
    output = subprocess.check_output(
        ["git", "-C", str(repository), "log", "--no-merges", "--format=@@@%x09%aE%x09%as", "--numstat"],
        text=True,
        encoding="utf-8",
    )
    statistics = defaultdict(lambda: {"commits": 0, "added": 0, "removed": 0})
    monthly_commits: dict[str, dict[str, int]] = defaultdict(lambda: defaultdict(int))
    current_handle: str | None = None

    for line in output.splitlines():
        if line.startswith("@@@\t"):
            _, author_email, authored_on = line.split("\t", 2)
            current_handle = aliases.get(author_email.lower())
            if current_handle:
                statistics[current_handle]["commits"] += 1
                monthly_commits[current_handle][authored_on[:7]] += 1
            continue

        fields = line.split("\t", 2)
        if current_handle and len(fields) == 3 and fields[0].isdigit() and fields[1].isdigit():
            statistics[current_handle]["added"] += int(fields[0])
            statistics[current_handle]["removed"] += int(fields[1])

    return (
        {handle: statistics[handle] for handle in CONTRIBUTORS},
        {handle: dict(monthly_commits[handle]) for handle in CONTRIBUTORS},
    )


def display_number(value: int, prefix: str = "") -> str:
    return f"{prefix}{value:,}".replace(",", ".")


def contribution_months(monthly_commits: dict[str, dict[str, int]], count: int = 16) -> list[str]:
    recorded_months = [month for commits in monthly_commits.values() for month in commits]
    latest = max(recorded_months, default=date.today().strftime("%Y-%m"))
    year, month = map(int, latest.split("-"))
    timeline: list[str] = []
    for _ in range(count):
        timeline.append(f"{year:04d}-{month:02d}")
        month -= 1
        if month == 0:
            year -= 1
            month = 12
    return list(reversed(timeline))


def month_label(month: str) -> str:
    months = ("jan", "fev", "mar", "abr", "mai", "jun", "jul", "ago", "set", "out", "nov", "dez")
    year, number = month.split("-")
    return f"{months[int(number) - 1]} {year}"


def render_card(
    handle: str,
    stats: dict[str, int],
    avatar_data_uri: str,
    months: list[str],
    monthly_commits: dict[str, int],
) -> str:
    safe_handle = html.escape(handle)
    commits = display_number(stats["commits"])
    added = display_number(stats["added"], "+")
    removed = display_number(stats["removed"], "−")
    values = [monthly_commits.get(month, 0) for month in months]
    highest = max(values, default=1) or 1
    chart_x, chart_y, chart_width, chart_height = 40, 156, 626, 120
    slot = chart_width / len(values)
    bars: list[str] = []
    for index, value in enumerate(values):
        bar_height = max(5, round(chart_height * value / highest))
        x = chart_x + index * slot + (slot - 16) / 2
        y = chart_y + chart_height - bar_height
        bars.append(
            f'''  <text x="{x + 8:.1f}" y="{y - 7}" class="chart-value" text-anchor="middle">{value}</text>
  <rect x="{x:.1f}" y="{y}" width="16" height="{bar_height}" rx="8" fill="url(#bar)" filter="url(#bar-glow)"/>'''
        )
    labels = ((chart_x, months[0]), (chart_x + chart_width / 2, months[len(months) // 2]), (chart_x + chart_width, months[-1]))
    date_labels = "\n".join(
        f'  <text x="{x:.1f}" y="322" class="date" text-anchor="{anchor}">{month_label(month)}</text>'
        for (x, month), anchor in zip(labels, ("start", "middle", "end"))
    )
    return f'''<svg xmlns="http://www.w3.org/2000/svg" width="720" height="360" viewBox="0 0 720 360" role="img" aria-labelledby="title desc">
  <title id="title">Contribuições de {safe_handle} no SistemaBiblioteca</title>
  <desc id="desc">{commits} commits, {added} linhas adicionadas e {removed} linhas removidas no repositório SistemaBiblioteca.</desc>
  <style>
    .handle {{ font: 700 34px 'Segoe UI', Arial, sans-serif; fill: #169bff; letter-spacing: -.8px; }}
    .metric {{ font: 700 24px 'Segoe UI', Arial, sans-serif; }}
    .commits {{ fill: #b8cbf5; }}
    .added {{ fill: #24e797; }}
    .removed {{ fill: #ff4969; }}
    .chart-value {{ font: 700 12px 'Segoe UI', Arial, sans-serif; fill: #d9e8ff; }}
    .date {{ font: 500 13px 'Segoe UI', Arial, sans-serif; fill: #a9c3ed; }}
    .axis {{ font: 500 12px 'Segoe UI', Arial, sans-serif; fill: #a9c3ed; }}
  </style>
  <defs>
    <radialGradient id="background" cx="50%" cy="5%" r="100%"><stop stop-color="#142b4a"/><stop offset=".52" stop-color="#071321"/><stop offset="1" stop-color="#030a12"/></radialGradient>
    <linearGradient id="border" x1="0" x2="1" y1="0" y2="1"><stop stop-color="#1ca4ff"/><stop offset=".5" stop-color="#3566ff"/><stop offset="1" stop-color="#24a2f8"/></linearGradient>
    <linearGradient id="bar" x1="0" x2="0" y1="1" y2="0"><stop stop-color="#087fe9"/><stop offset="1" stop-color="#22b8ff"/></linearGradient>
    <filter id="outer-glow" x="-15%" y="-20%" width="130%" height="140%"><feGaussianBlur stdDeviation="7" result="blur"/><feMerge><feMergeNode in="blur"/><feMergeNode in="SourceGraphic"/></feMerge></filter>
    <filter id="bar-glow" x="-80%" y="-30%" width="260%" height="160%"><feGaussianBlur stdDeviation="2.2" result="blur"/><feMerge><feMergeNode in="blur"/><feMergeNode in="SourceGraphic"/></feMerge></filter>
    <clipPath id="avatar"><circle cx="73" cy="73" r="38"/></clipPath>
  </defs>
  <rect x="1" y="1" width="718" height="358" rx="22" fill="url(#background)" stroke="url(#border)" stroke-width="2" filter="url(#outer-glow)"/>
  <circle cx="73" cy="73" r="41" fill="none" stroke="#239cff" stroke-width="3" filter="url(#outer-glow)"/>
  <image href="{avatar_data_uri}" x="35" y="35" width="76" height="76" clip-path="url(#avatar)" preserveAspectRatio="xMidYMid slice"/>
  <text x="132" y="67" class="handle">{safe_handle}</text>
  <text x="132" y="108" class="metric commits">{commits} commits</text>
  <text x="322" y="108" class="metric added">{added} ++</text>
  <text x="476" y="108" class="metric removed">{removed} --</text>
  <g stroke="#376087" stroke-width="1" stroke-dasharray="5 6" opacity=".82">
    <line x1="40" y1="156" x2="666" y2="156"/><line x1="40" y1="216" x2="666" y2="216"/><line x1="40" y1="276" x2="666" y2="276"/>
    <line x1="40" y1="156" x2="40" y2="294"/><line x1="353" y1="156" x2="353" y2="294"/><line x1="666" y1="156" x2="666" y2="294"/>
  </g>
  <line x1="40" y1="276" x2="666" y2="276" stroke="#118ff1" stroke-width="2" filter="url(#bar-glow)"/>
{chr(10).join(bars)}
  <text x="682" y="166" class="axis" transform="rotate(90 682 166)">Contribuições</text>
  <text x="674" y="280" class="axis">0</text>
  <text x="674" y="220" class="axis">{max(1, round(highest / 2))}</text>
  <text x="674" y="160" class="axis">{highest}</text>
{date_labels}
</svg>
'''


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--repository", type=Path, default=Path.cwd())
    parser.add_argument("--output", type=Path, default=Path("assets/contribuicoes"))
    args = parser.parse_args()

    destination = args.repository / args.output
    destination.mkdir(parents=True, exist_ok=True)
    statistics, monthly_commits = collect_statistics(args.repository)
    months = contribution_months(monthly_commits)
    for handle, contributor_statistics in statistics.items():
        avatar_data_uri = load_avatar_data_uri(handle)
        (destination / f"{handle}.svg").write_text(
            render_card(handle, contributor_statistics, avatar_data_uri, months, monthly_commits[handle]),
            encoding="utf-8",
        )


if __name__ == "__main__":
    main()
