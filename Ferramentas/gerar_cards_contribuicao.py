#!/usr/bin/env python3
"""Gera cards SVG das contribuições da equipe a partir do histórico Git."""

from __future__ import annotations

import argparse
import html
import subprocess
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


def collect_statistics(repository: Path) -> dict[str, dict[str, int]]:
    aliases = {email.lower(): handle for handle, emails in CONTRIBUTORS.items() for email in emails}
    output = subprocess.check_output(
        ["git", "-C", str(repository), "log", "--no-merges", "--format=@@@%x09%aE", "--numstat"],
        text=True,
        encoding="utf-8",
    )
    statistics = defaultdict(lambda: {"commits": 0, "added": 0, "removed": 0})
    current_handle: str | None = None

    for line in output.splitlines():
        if line.startswith("@@@\t"):
            current_handle = aliases.get(line.removeprefix("@@@\t").lower())
            if current_handle:
                statistics[current_handle]["commits"] += 1
            continue

        fields = line.split("\t", 2)
        if current_handle and len(fields) == 3 and fields[0].isdigit() and fields[1].isdigit():
            statistics[current_handle]["added"] += int(fields[0])
            statistics[current_handle]["removed"] += int(fields[1])

    return {handle: statistics[handle] for handle in CONTRIBUTORS}


def display_number(value: int, prefix: str = "") -> str:
    return f"{prefix}{value:,}".replace(",", ".")


def render_card(handle: str, stats: dict[str, int]) -> str:
    safe_handle = html.escape(handle)
    commits = display_number(stats["commits"])
    added = display_number(stats["added"], "+")
    removed = display_number(stats["removed"], "−")
    return f'''<svg xmlns="http://www.w3.org/2000/svg" width="1000" height="270" viewBox="0 0 1000 270" role="img" aria-labelledby="title desc">
  <title id="title">Contribuições de {safe_handle} no SUB</title>
  <desc id="desc">{commits} commits, {added} linhas adicionadas e {removed} linhas removidas no repositório SistemaBiblioteca.</desc>
  <style>
    .handle {{ font: 700 34px 'Segoe UI', Arial, sans-serif; fill: #ffffff; }}
    .label {{ font: 15px 'Segoe UI', Arial, sans-serif; fill: #60758c; }}
    .value {{ font: 700 31px 'Segoe UI', Arial, sans-serif; }}
  </style>
  <rect width="1000" height="270" rx="18" fill="#eef4fa"/>
  <path d="M0 18Q0 0 18 0H982Q1000 0 1000 18V100H0Z" fill="#1f487e"/>
  <text x="48" y="61" class="handle">{safe_handle}</text>
  <rect x="48" y="132" width="278" height="94" rx="14" fill="#ffffff"/>
  <rect x="361" y="132" width="278" height="94" rx="14" fill="#ffffff"/>
  <rect x="674" y="132" width="278" height="94" rx="14" fill="#ffffff"/>
  <text x="74" y="163" class="label">Commits</text>
  <text x="74" y="202" class="value" fill="#1f487e">{commits}</text>
  <text x="387" y="163" class="label">Linhas adicionadas</text>
  <text x="387" y="202" class="value" fill="#16803c">{added}</text>
  <text x="700" y="163" class="label">Linhas removidas</text>
  <text x="700" y="202" class="value" fill="#bb3e32">{removed}</text>
</svg>
'''


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--repository", type=Path, default=Path.cwd())
    parser.add_argument("--output", type=Path, default=Path("assets/contribuicoes"))
    args = parser.parse_args()

    destination = args.repository / args.output
    destination.mkdir(parents=True, exist_ok=True)
    for handle, statistics in collect_statistics(args.repository).items():
        (destination / f"{handle}.svg").write_text(render_card(handle, statistics), encoding="utf-8")


if __name__ == "__main__":
    main()
