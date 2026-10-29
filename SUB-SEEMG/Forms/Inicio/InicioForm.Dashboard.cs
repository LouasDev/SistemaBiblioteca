using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SUB.Utils;

namespace SUB.Forms.Inicio
{
    public partial class InicioForm
    {
        // TabControl pintado por inteiro: com OwnerDrawFixed o Windows ainda desenhava
        // a moldura 3D clássica, com bordas escuras em volta das abas.
        private sealed class AbasPlanas : TabControl
        {
            private readonly Func<Color> corDoTema;
            private readonly System.Drawing.Font fonteSelecionada = new System.Drawing.Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            public AbasPlanas(Func<Color> corDoTema)
            {
                this.corDoTema = corDoTema;
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            }

            protected override void OnSelectedIndexChanged(EventArgs e)
            {
                base.OnSelectedIndexChanged(e);
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Color tema = ThemeManager.Normalize(corDoTema());
                bool escuro = ThemeManager.Escuro;
                Color linha = escuro ? ThemeManager.Dark.Borda : ThemeManager.Lighten(tema, 0.72F);
                Color fundoAtiva = escuro ? ThemeManager.Dark.Superficie : Color.White;
                Color fundoInativa = escuro ? ThemeManager.Dark.Fundo : ThemeManager.Lighten(tema, 0.93F);
                Color textoInativa = escuro ? ThemeManager.Dark.TextoSuave : Color.FromArgb(92, 100, 112);
                Color destaqueAtiva = escuro ? ThemeManager.Lighten(tema, 0.35F) : tema;
                e.Graphics.Clear(Parent != null ? Parent.BackColor : fundoAtiva);
                if (TabCount == 0)
                    return;
                int topoPagina = GetTabRect(0).Bottom;
                using (var caneta = new Pen(linha))
                    e.Graphics.DrawRectangle(caneta, 0, topoPagina, Width - 1, Height - topoPagina - 1);
                for (int i = 0; i < TabCount; i++)
                {
                    System.Drawing.Rectangle aba = GetTabRect(i);
                    aba = new System.Drawing.Rectangle(aba.X + 1, aba.Y + 2, aba.Width - 2, topoPagina - aba.Y - 2);
                    bool selecionada = i == SelectedIndex;
                    using (var pincel = new SolidBrush(selecionada ? fundoAtiva : fundoInativa))
                        e.Graphics.FillRectangle(pincel, aba);
                    if (selecionada)
                    {
                        using (var destaque = new SolidBrush(destaqueAtiva))
                            e.Graphics.FillRectangle(destaque, aba.X, aba.Y, aba.Width, 3);
                        using (var caneta = new Pen(linha))
                        {
                            e.Graphics.DrawLine(caneta, aba.Left, aba.Top, aba.Left, aba.Bottom);
                            e.Graphics.DrawLine(caneta, aba.Right - 1, aba.Top, aba.Right - 1, aba.Bottom);
                        }

                        using (var apagaLinha = new Pen(fundoAtiva))
                            e.Graphics.DrawLine(apagaLinha, aba.Left + 1, topoPagina, aba.Right - 2, topoPagina);
                    }

                    TextRenderer.DrawText(e.Graphics, TabPages[i].Text, selecionada ? fonteSelecionada : Font, new System.Drawing.Rectangle(aba.X, aba.Y + 2, aba.Width, aba.Height - 2), selecionada ? ThemeManager.GetLabelColor(tema) : textoInativa, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    fonteSelecionada.Dispose();
                base.Dispose(disposing);
            }
        }

        private void ConstruirDashboardUI()
        {
            // remover controles dinâmicos antigos do panel1 (se houver)
            foreach (Control c in panel1.Controls.OfType<Control>().ToArray())
            {
                if (c != topPanelInside && !(c is TabControl && c.Name == "tabEstatisticas"))
                {
                    panel1.Controls.Remove(c);
                    c.Dispose();
                }
            }

            foreach (Control card in flowCards.Controls.OfType<Control>().ToArray())
            {
                flowCards.Controls.Remove(card);
                card.Dispose();
            }

            var cardsInfo = new[]
            {
                new
                {
                    Key = "Usuarios",
                    Title = "Usuários",
                    Sub = "Total de usuários",
                    Color = Color.FromArgb(30, 61, 88)
                },
                new
                {
                    Key = "Livros",
                    Title = "Acervo",
                    Sub = "Total de livros",
                    Color = Color.FromArgb(9, 74, 158)
                },
                new
                {
                    Key = "EmpAtivos",
                    Title = "Empréstimos ativos",
                    Sub = "Empréstimos não devolvidos",
                    Color = Color.FromArgb(34, 139, 34)
                },
                new
                {
                    Key = "Atrasados",
                    Title = "Atrasados",
                    Sub = "Empréstimos em atraso",
                    Color = Color.FromArgb(178, 34, 34)
                },
                new
                {
                    Key = "RapidosHoje",
                    Title = "Rápidos hoje",
                    Sub = "Empréstimos rápidos hoje",
                    Color = Color.FromArgb(92, 92, 205)
                },
                new
                {
                    Key = "MediaMes",
                    Title = "Média mensal",
                    Sub = "Média de empréstimos deste mês",
                    Color = Color.FromArgb(255, 140, 0)
                },
                new
                {
                    Key = "TopUsuario",
                    Title = "Leitor mais ativo",
                    Sub = "Usuário que mais emprestou",
                    Color = Color.FromArgb(0, 123, 167)
                }
            };
            // Calcula o tamanho ideal para os cards
            int cardWidth = 210;
            int cardHeight = 110;
            foreach (var c in cardsInfo)
                flowCards.Controls.Add(CriarCard(c.Key, c.Title, c.Sub, c.Color, cardWidth, cardHeight));
            flowCards.AutoScroll = false;
            flowCards.WrapContents = true;
            topPanelInside.Resize -= TopPanelInside_Resize;
            topPanelInside.Resize += TopPanelInside_Resize;
            // === ACTIONS PANEL (topo direito) com botão visível e maior ===
            if (actionsPanel != null)
            {
                panelTop.Controls.Remove(actionsPanel);
                actionsPanel.Dispose();
            }

            actionsPanel = new Panel
            {
                Name = "actionsPanel",
                Size = new Size(260, 64),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(panelTop.Width - 280, 12),
                BackColor = Color.Transparent
            };
            panelTop.Controls.Add(actionsPanel);
            actionsPanel.BringToFront();
            var btnFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoSize = false,
                Padding = new Padding(8),
                Margin = new Padding(0)
            };
            actionsPanel.Controls.Add(btnFlow);
            // Botão de Empréstimo Rápido (maior, topo direito, com leve arredondamento)
            btnEmprestimoRapido = new Button
            {
                Text = "Empréstimo Rápido",
                AutoSize = false,
                Size = new Size(220, 44), // Aumentado
                BackColor = Color.FromArgb(9, 74, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                AccessibleName = "Empréstimo Rápido",
                Cursor = Cursors.Hand
            };
            btnEmprestimoRapido.FlatAppearance.BorderSize = 0;
            btnEmprestimoRapido.Click += BtnEmprestimoRapido_Click;
            // aplicar cantos arredondados (suave)
            btnEmprestimoRapido.Paint += (s, e) =>
            {
                var btn = s as Button;
                if (btn == null)
                    return;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = RoundedRect(new System.Drawing.Rectangle(0, 0, btn.Width, btn.Height), 8))
                using (var brush = new SolidBrush(btn.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // desenhar texto manualmente para garantir centralização
                TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, new System.Drawing.Rectangle(0, 0, btn.Width, btn.Height), btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
            // esconder border padrão para evitar sobreposição
            btnEmprestimoRapido.FlatAppearance.BorderSize = 0;
            btnFlow.Controls.Add(btnEmprestimoRapido);
            formToolTip.SetToolTip(btnEmprestimoRapido, "Abrir Empréstimo Rápido (atalho: Ctrl+R)");
            // reajusta posição quando redimensionar o panelTop
            panelTop.Resize += (s, e) =>
            {
                actionsPanel.Location = new Point(Math.Max(12, panelTop.Width - actionsPanel.Width - 20), actionsPanel.Location.Y);
                CenterClock(); // reposicionar relógio para centro
            };
            // lblStatusSmall (texto de status) – posicionado logo abaixo dos cards
            if (lblStatusSmall != null)
            {
                panel1.Controls.Remove(lblStatusSmall);
                lblStatusSmall.Dispose();
            }

            lblStatusSmall = new Label
            {
                Name = "lblStatusSmall",
                Text = "",
                AutoSize = true,
                Location = new Point(18, topPanelInside.Bottom + 6),
                ForeColor = Color.Gray,
                Font = new System.Drawing.Font("Segoe UI", 9F),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            panel1.Controls.Add(lblStatusSmall);
            AjustarCards();
            // === TabControl moderno ===
            var tabEstatisticas = new AbasPlanas(() => dashboardThemeColor)
            {
                Name = "tabEstatisticas",
                Dock = DockStyle.Fill,
                ItemSize = new Size(150, 36),
                SizeMode = TabSizeMode.Fixed,
                Font = new System.Drawing.Font("Segoe UI", 9.75F)
            };
            var tabDevedores = new TabPage("Devedores")
            {
                BackColor = Color.White,
                Padding = new Padding(12)
            };
            var tabEstEmp = new TabPage("Empréstimos")
            {
                BackColor = Color.White,
                Padding = new Padding(12)
            };
            var tabLivros = new TabPage("Livros Populares")
            {
                BackColor = Color.White,
                Padding = new Padding(12)
            };
            tabEstatisticas.TabPages.Add(tabDevedores);
            tabEstatisticas.TabPages.Add(tabEstEmp);
            tabEstatisticas.TabPages.Add(tabLivros);
            // DataGrids com margin top aumentado para dar 'respiração'
            int topMarginDataGrid = 36; // já aumentado conforme seu pedido
            var dgvDevedores = CriarDataGridBasico("dgvDevedores");
            dgvDevedores.Margin = new Padding(12, topMarginDataGrid, 12, 12);
            dgvDevedores.Columns.Clear();
            dgvDevedores.Columns.AddRange(new DataGridViewColumn[] { new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Nome", DataPropertyName = "Nome", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill }, new DataGridViewTextBoxColumn { Name = "Turma", HeaderText = "Turma", DataPropertyName = "Turma", Width = 250 }, new DataGridViewTextBoxColumn { Name = "Livro", HeaderText = "Livro", DataPropertyName = "Livro", Width = 250 }, new DataGridViewTextBoxColumn { Name = "DataDevolucao", HeaderText = "Data Devolução", DataPropertyName = "DataDevolucao", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } }, new DataGridViewTextBoxColumn { Name = "DiasAtraso", HeaderText = "Dias em Atraso", DataPropertyName = "DiasAtraso", Width = 130 } });
            dgvDevedores.Columns.Add(new DataGridViewButtonColumn { Name = "btnComunicado", HeaderText = "", Text = "Gerar comunicado", UseColumnTextForButtonValue = true, Width = 145, FlatStyle = FlatStyle.Flat });
            dgvDevedores.CellContentClick += dgvDevedores_CellContentClick;
            dgvDevedores.CellPainting += dgvDevedores_CellPainting;
            tabDevedores.Controls.Add(dgvDevedores);
            var dgvEstatEmp = CriarDataGridBasico("dgvEstatisticasEmprestimos");
            dgvEstatEmp.Margin = new Padding(12, topMarginDataGrid, 12, 12);
            dgvEstatEmp.Columns.AddRange(new DataGridViewColumn[] { new DataGridViewTextBoxColumn { Name = "Categoria", HeaderText = "Categoria", DataPropertyName = "Categoria", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill }, new DataGridViewTextBoxColumn { Name = "Valor", HeaderText = "Valor", DataPropertyName = "Valor", Width = 100 }, new DataGridViewTextBoxColumn { Name = "Detalhes", HeaderText = "Detalhes", DataPropertyName = "Detalhes", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill } });
            dgvEstatEmp.CellFormatting += DgvEstatisticasEmprestimos_CellFormatting;
            tabEstEmp.Controls.Add(dgvEstatEmp);
            // === Livros Populares: ajustar colunas para evitar truncamento do título ===
            var dgvLivrosPop = CriarDataGridBasico("dgvLivrosPopulares");
            dgvLivrosPop.Margin = new Padding(12, topMarginDataGrid, 12, 12);
            // Fazemos colunas com sizing misto:
            dgvLivrosPop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // gerenciamos larguras manualmente
            var colRanking = new DataGridViewTextBoxColumn
            {
                Name = "Ranking",
                HeaderText = "Ranking",
                DataPropertyName = "Posicao",
                Width = 100
            };
            var colTitulo = new DataGridViewTextBoxColumn
            {
                Name = "Titulo",
                HeaderText = "Título",
                DataPropertyName = "Titulo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 260
            };
            var colAutor = new DataGridViewTextBoxColumn
            {
                Name = "Autor",
                HeaderText = "Autor",
                DataPropertyName = "Autor",
                Width = 200
            };
            var colEmp = new DataGridViewTextBoxColumn
            {
                Name = "Emprestimos",
                HeaderText = "Empréstimos",
                DataPropertyName = "Emprestimos",
                Width = 100
            };
            var colDisp = new DataGridViewTextBoxColumn
            {
                Name = "Disponibilidade",
                HeaderText = "Status",
                DataPropertyName = "Disponibilidade",
                Width = 220
            };
            dgvLivrosPop.CellFormatting += (s, e) =>
            {
                var grid = (DataGridView)s;
                if (grid.Columns[e.ColumnIndex].Name == "Ranking" && e.Value != null)
                {
                    int pos;
                    if (int.TryParse(e.Value.ToString(), out pos))
                    {
                        if (pos == 1)
                        {
                            e.CellStyle.ForeColor = Color.Gold; // dourado
                            e.CellStyle.Font = new System.Drawing.Font(grid.Font, FontStyle.Bold);
                            e.Value = "🏆 #1"; // troféu e #1
                        }
                        else
                        {
                            e.Value = $"#{pos}";
                            e.CellStyle.ForeColor = ThemeManager.Escuro ? ThemeManager.Dark.Texto : Color.Black;
                            e.CellStyle.Font = grid.Font;
                        }

                        e.FormattingApplied = true;
                    }
                }
            };
            // adiciona na ordem
            dgvLivrosPop.Columns.AddRange(new DataGridViewColumn[] { colRanking, colTitulo, colAutor, colEmp, colDisp });
            // Depois que o controle estiver em tela, definimos colTitulo como Fill para aproveitar espaço restante.
            // (Aplicamos FillWeight para priorizar bastante espaço ao título)
            colTitulo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTitulo.FillWeight = 150; // maior proporção de espaço
            colAutor.FillWeight = 40;
            colEmp.FillWeight = 20;
            colDisp.FillWeight = 30;
            // Ajustes visuais
            colRanking.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTitulo.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            colTitulo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colAutor.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colEmp.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDisp.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tabLivros.Controls.Add(dgvLivrosPop);
            panel1.Controls.Add(tabEstatisticas);
            tabEstatisticas.BringToFront();
            // status inicial
            SetStatus("Pronto.");
            // aumentar e centralizar visual das labels do header: saudação e relógio
            try
            {
                lblOla.Font = new System.Drawing.Font("Segoe UI", 20F, FontStyle.Bold); // aumentada
                lblOla.ForeColor = ThemeManager.GetLabelColor(dashboardThemeColor);
                lblRelogio.Font = new System.Drawing.Font("Segoe UI", 16F, FontStyle.Regular); // já aumentado anteriormente
                lblRelogio.ForeColor = ThemeManager.Escuro ? ThemeManager.Dark.TextoSuave : Color.FromArgb(60, 60, 60);
                // garantir que o relógio seja centralizado no panelTop
                CenterClock();
            }
            catch
            { /* ignore se labels não existirem no designer */
            }

            AplicarTemaNoDashboard();
        }

        private void AplicarTemaNoDashboard()
        {
            Color text = ThemeManager.GetLabelColor(dashboardThemeColor);
            if (btnEmprestimoRapido != null)
            {
                btnEmprestimoRapido.BackColor = Color.FromArgb(9, 74, 158);
                btnEmprestimoRapido.ForeColor = Color.White;
                btnEmprestimoRapido.FlatAppearance.MouseOverBackColor = Color.FromArgb(7, 62, 132);
            }

            if (flowCards == null)
                return;
            foreach (Panel card in flowCards.Controls.OfType<Panel>())
            {
                card.BackColor = ThemeManager.Escuro ? ThemeManager.Dark.Superficie : Color.White;
                if (ThemeManager.Escuro)
                    PintarInternosDoCard(card, card.BackColor);
                card.Invalidate();
                Panel header = card.Controls.OfType<Panel>().FirstOrDefault(p => p.Dock == DockStyle.Top);
                string key = card.Tag as string;
                if (header != null)
                {
                    if (string.Equals(key, "Usuarios", StringComparison.OrdinalIgnoreCase) || string.Equals(key, "Livros", StringComparison.OrdinalIgnoreCase))
                    {
                        header.BackColor = dashboardThemeColor;
                    }

                    Label headerLabel = header.Controls.OfType<Label>().FirstOrDefault();
                    if (headerLabel != null)
                        headerLabel.ForeColor = ThemeManager.GetContrastColor(header.BackColor);
                }

                var value = card.Controls.Find("val_" + key, true).FirstOrDefault() as Label;
                if (value != null)
                    value.ForeColor = text;
            }
        }

        // Painéis internos (corpo do card do "Leitor mais ativo") nascem brancos; o cabeçalho colorido fica.
        private void PintarInternosDoCard(Control raiz, Color fundo)
        {
            foreach (Control filho in raiz.Controls)
            {
                var painel = filho as Panel;
                if (painel != null && painel.Dock != DockStyle.Top)
                    painel.BackColor = fundo;
                PintarInternosDoCard(filho, fundo);
            }
        }

        // Mesmo botão arredondado na cor do tema usado nas grades de Acervo, Pessoas e Devoluções.
        private void dgvDevedores_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0 || e.ColumnIndex < 0 || grid.Columns[e.ColumnIndex].Name != "btnImprimir")
                return;
            e.PaintBackground(e.CellBounds, true);
            var rect = new System.Drawing.Rectangle(e.CellBounds.X + 6, e.CellBounds.Y + 5, e.CellBounds.Width - 12, e.CellBounds.Height - 10);
            var suavizacaoAnterior = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = RoundedRect(rect, 8))
            using (var brush = new SolidBrush(grid.ColumnHeadersDefaultCellStyle.BackColor))
                e.Graphics.FillPath(brush, path);
            e.Graphics.SmoothingMode = suavizacaoAnterior;
            using (var fonte = new System.Drawing.Font("Segoe UI Semibold", 8.5F))
                TextRenderer.DrawText(e.Graphics, "Imprimir carta", fonte, rect, grid.ColumnHeadersDefaultCellStyle.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            e.Handled = true;
        }

        private void dgvDevedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv == null || e.RowIndex < 0)
                return;
            if (dgv.Columns[e.ColumnIndex].Name == "btnComunicado")
            {
                var emprestimo = dgv.Rows[e.RowIndex].DataBoundItem as EmprestimoAtrasadoInfo;
                if (emprestimo == null)
                {
                    MessageBox.Show("Não foi possível obter os dados do empréstimo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Busca todos os livros atrasados do aluno
                var livros = ObterItensAtrasadosPorUsuario(emprestimo.UsuarioId);
                GerarComunicadoAtrasoPdf(emprestimo, livros);
            }
        }

        // Helper para desenhar retângulo arredondado
        private GraphicsPath RoundedRect(System.Drawing.Rectangle bounds, int radius)
        {
            var gp = new GraphicsPath();
            int d = radius * 2;
            gp.AddArc(bounds.Left, bounds.Top, d, d, 180, 90);
            gp.AddArc(bounds.Right - d, bounds.Top, d, d, 270, 90);
            gp.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            gp.AddArc(bounds.Left, bounds.Bottom - d, d, d, 90, 90);
            gp.CloseFigure();
            return gp;
        }

        // Ouve o painel de cima (e não o de cartões): mudar a altura do pai durante o layout do
        // filho não é reaplicado aos controles encaixados, e a segunda linha ficava cortada.
        private void TopPanelInside_Resize(object sender, EventArgs e)
        {
            AjustarCards();
        }

        // Os sete cartões preenchem a linha inteira; quando não cabem com uma largura legível
        // (telas de 1366 px), passam para duas linhas em vez de exigir rolagem lateral.
        private void AjustarCards()
        {
            var cards = flowCards.Controls.OfType<Panel>().ToList();
            if (cards.Count == 0 || topPanelInside.ClientSize.Width <= 0)
                return;
            const int larguraMinima = 180;
            int espaco = cards[0].Margin.Horizontal;
            int disponivel = topPanelInside.ClientSize.Width - topPanelInside.Padding.Horizontal - flowCards.Padding.Horizontal;
            int porLinha = cards.Count;
            while (porLinha > 1 && disponivel / porLinha - espaco < larguraMinima)
                porLinha = (porLinha + 1) / 2;
            int largura = disponivel / porLinha - espaco;
            int linhas = (cards.Count + porLinha - 1) / porLinha;
            if (alturaCardsDesenho == 0)
                alturaCardsDesenho = topPanelInside.Height;
            flowCards.SuspendLayout();
            foreach (var card in cards)
            {
                card.Width = largura;
                foreach (var valor in card.Controls.Find("val_" + card.Tag, true).OfType<Label>())
                    valor.Width = largura - 24;
                foreach (var nome in card.Controls.Find("lblTopName", true).Concat(card.Controls.Find("lblTopTurma", true)).OfType<Label>())
                    nome.MaximumSize = new Size(largura - 40, 0);
            }

            flowCards.ResumeLayout();
            int altura = alturaCardsDesenho + (linhas - 1) * (cards[0].Height + cards[0].Margin.Vertical);
            if (topPanelInside.Height != altura)
                topPanelInside.Height = altura;
            if (lblStatusSmall != null)
                lblStatusSmall.Top = topPanelInside.Bottom + 6;
        }

        private Panel CriarCard(string key, string title, string subtitle, Color headerColor, int cardWidth, int cardHeight)
        {
            var card = new Panel
            {
                Width = cardWidth,
                Height = cardHeight,
                Margin = new Padding(8),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(1), // reserva 1px para a borda
                Tag = key
            };
            card.Paint += (s, e) =>
            {
                using (var p = new Pen(ThemeManager.Escuro ? ThemeManager.Dark.Borda : Color.FromArgb(235, 239, 244)))
                    e.Graphics.DrawRectangle(p, 0, 0, card.Width - 1, card.Height - 1);
            };
            var header = new Panel
            {
                BackColor = headerColor,
                Height = 36,
                Dock = DockStyle.Top,
                Margin = new Padding(0)
            };
            card.Controls.Add(header);
            var lblTitle = new Label
            {
                Text = title,
                ForeColor = Color.White,
                Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                Location = new Point(10, 6),
                AutoSize = true
            };
            header.Controls.Add(lblTitle);
            // ---------- Card padrão ----------
            if (!string.Equals(key, "TopUsuario", StringComparison.OrdinalIgnoreCase))
            {
                var lblValue = new Label
                {
                    Name = "val_" + key,
                    Text = "0",
                    ForeColor = Color.FromArgb(20, 42, 60),
                    Font = new System.Drawing.Font("Segoe UI", 20F, FontStyle.Bold),
                    Location = new Point(12, header.Bottom + 6),
                    AutoSize = false,
                    Size = new Size(card.Width - 24, 36)
                };
                card.Controls.Add(lblValue);
                var lblSub = new Label
                {
                    Text = subtitle,
                    ForeColor = Color.Gray,
                    Font = new System.Drawing.Font("Segoe UI", 8.5F),
                    Location = new Point(12, lblValue.Bottom + 2),
                    AutoSize = true
                };
                card.Controls.Add(lblSub);
            }
            else
            {
                // ---------- Card Top Usuário com TableLayoutPanel para vertical centering ----------
                var container = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    Margin = new Padding(0),
                    Padding = new Padding(8, 43, 8, 0) // leve padding interno
                };
                var table = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    ColumnCount = 1,
                    RowCount = 4
                };
                // linhas: flex (30%), nome (Auto), turma (Auto), flex (30%)
                table.RowStyles.Clear();
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 30f));
                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 30f));
                // Label do nome: dois primeiros nomes (maior)
                var lblName = new Label
                {
                    Name = "lblTopName",
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new System.Drawing.Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(20, 42, 60),
                    Text = "-",
                    MaximumSize = new Size(cardWidth - 40, 0) // evita overflow horizontal
                };
                // centralizar horizontalmente na célula
                lblName.Anchor = AnchorStyles.None;
                // Label da turma: menor e logo abaixo do nome (mais próximo)
                var lblTurma = new Label
                {
                    Name = "lblTopTurma",
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Regular),
                    ForeColor = Color.Gray,
                    Text = "",
                    MaximumSize = new Size(cardWidth - 40, 0),
                };
                lblTurma.Anchor = AnchorStyles.None;
                // Subtítulo embaixo (quem mais emprestou)
                var lblSub = new Label
                {
                    Text = subtitle,
                    Dock = DockStyle.Bottom,
                    Height = 35,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Gray,
                    Font = new System.Drawing.Font("Segoe UI", 8.5F)
                };
                // adicionar aos lugares corretos: row 1 = nome, row 2 = turma
                table.Controls.Add(lblName, 0, 1);
                table.Controls.Add(lblTurma, 0, 2);
                container.Controls.Add(table);
                container.Controls.Add(lblSub); // ficará embaixo do container (Dock = Bottom do lblSub)
                card.Controls.Add(container);
            }

            return card;
        }

        private void DgvEstatisticasEmprestimos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0 || e.ColumnIndex < 0 || grid.Columns[e.ColumnIndex].Name != "Valor")
            {
                return;
            }

            string categoria = grid.Rows[e.RowIndex].Cells["Categoria"].Value as string;
            if (!string.Equals(categoria, "Devolução no Prazo", StringComparison.Ordinal) || e.Value == null)
                return;
            if (double.TryParse(e.Value.ToString(), out double taxa))
            {
                e.Value = taxa.ToString("0.#") + "%";
                e.FormattingApplied = true;
            }
        }

        private DataGridView CriarDataGridBasico(string name)
        {
            var dgv = new DataGridView
            {
                Name = name,
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToResizeRows = false,
                RowTemplate =
                {
                    Height = 36
                },
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 40,
                EnableHeadersVisualStyles = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                GridColor = Color.FromArgb(245, 245, 245),
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                Margin = new Padding(12)
            };
            AplicarEstiloDataGridView(dgv);
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#E7EEF7");
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(20, 42, 60);
            dgv.CellMouseEnter += DataGrid_CellMouseEnter;
            dgv.CellMouseLeave += DataGrid_CellMouseLeave;
            // Grade de aba oculta seleciona a primeira linha ao aparecer pela primeira vez,
            // escondendo a cor de status dela; no painel não há seleção a manter.
            dgv.HandleCreated += (s, e) => dgv.BeginInvoke(new Action(dgv.ClearSelection));
            dgv.VisibleChanged += (s, e) =>
            {
                if (dgv.Visible && dgv.IsHandleCreated)
                    dgv.BeginInvoke(new Action(dgv.ClearSelection));
            };
            return dgv;
        }

        private void AplicarEstiloDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.FromArgb(245, 245, 245);
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9f);
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#E7EEF7");
            dgv.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#123A5D");
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.DefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(70, 70, 70);
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, dgv, new object[] { true });
            }
            catch
            {
            }

            dgv.CellFormatting += (s, e) =>
            {
                try
                {
                    var grid = (DataGridView)s;
                    if (grid.Columns[e.ColumnIndex].Name == "Disponibilidade")
                    {
                        string v = e.Value?.ToString() ?? "";
                        bool escuro = ThemeManager.Escuro;
                        if (v == "Disponível")
                        {
                            e.CellStyle.BackColor = escuro ? Color.FromArgb(27, 62, 43) : Color.FromArgb(220, 245, 225);
                            e.CellStyle.ForeColor = escuro ? Color.FromArgb(134, 214, 156) : Color.FromArgb(20, 120, 40);
                            e.Value = "●  " + v;
                        }
                        else if (v == "Poucas unidades")
                        {
                            e.CellStyle.BackColor = escuro ? Color.FromArgb(68, 58, 24) : Color.FromArgb(255, 250, 220);
                            e.CellStyle.ForeColor = escuro ? Color.FromArgb(232, 192, 104) : Color.FromArgb(150, 110, 20);
                            e.Value = "●  " + v;
                        }
                        else
                        {
                            e.CellStyle.BackColor = escuro ? Color.FromArgb(74, 36, 38) : Color.FromArgb(255, 235, 235);
                            e.CellStyle.ForeColor = escuro ? Color.FromArgb(240, 142, 142) : Color.FromArgb(160, 30, 30);
                            e.Value = "●  " + v;
                        }

                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                catch
                {
                }
            };
        }

        private void DataGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                var dgv = sender as DataGridView;
                if (dgv == null)
                    return;
                var hoverColor = ThemeManager.Escuro ? ThemeManager.Dark.EntradaHover : Color.FromArgb(245, 248, 251);
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = hoverColor;
            }
            catch
            {
            }
        }

        private void DataGrid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                var dgv = sender as DataGridView;
                if (dgv == null)
                    return;
                var row = dgv.Rows[e.RowIndex];
                if (ThemeManager.Escuro)
                    row.DefaultCellStyle.BackColor = (e.RowIndex % 2 == 0) ? ThemeManager.Dark.Superficie : ThemeManager.Dark.SuperficieAlt;
                else
                    row.DefaultCellStyle.BackColor = (e.RowIndex % 2 == 0) ? Color.White : Color.FromArgb(250, 250, 250);
            }
            catch
            {
            }
        }

        private void EnsureEmptyOverlay(DataGridView dgv, string message)
        {
            var name = "emptyOverlay_" + dgv.Name;
            var parent = dgv.Parent ?? panel1;
            Label emptyLabel;
            if (!emptyGridLabels.TryGetValue(dgv, out emptyLabel) || emptyLabel.IsDisposed)
            {
                var legacyOverlay = parent.Controls[name] as Label;
                if (legacyOverlay != null)
                {
                    parent.Controls.Remove(legacyOverlay);
                    legacyOverlay.Dispose();
                }

                emptyLabel = new Label
                {
                    Name = name,
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = ThemeManager.Escuro ? ThemeManager.Dark.TextoSuave : Color.Gray,
                    BackColor = ThemeManager.Escuro ? ThemeManager.Dark.Superficie : Color.White,
                    Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Regular),
                    Visible = false
                };
                parent.Controls.Add(emptyLabel);
                emptyGridLabels[dgv] = emptyLabel;
                dgv.DataBindingComplete += (s, e) => AtualizarEmptyLabel(dgv);
            }

            emptyLabel.Text = message;
            AtualizarEmptyLabel(dgv);
        }

        private void AtualizarEmptyLabel(DataGridView dgv)
        {
            Label emptyLabel;
            if (dgv == null || dgv.IsDisposed || !emptyGridLabels.TryGetValue(dgv, out emptyLabel) || emptyLabel.IsDisposed)
                return;
            bool vazio = dgv.Rows.Count == 0;
            dgv.Visible = !vazio;
            emptyLabel.Visible = vazio;
            if (vazio)
                emptyLabel.BringToFront();
            else
                dgv.BringToFront();
        }

        private void AtualizarCards(Dictionary<string, int> stats)
        {
            if (stats == null)
                return;
            foreach (Control c in flowCards.Controls)
            {
                if (c is Panel card && card.Tag is string key)
                {
                    // não sobrescrever o card "TopUsuario" aqui (ele mostra texto, não número)
                    if (string.Equals(key, "TopUsuario", StringComparison.OrdinalIgnoreCase))
                        continue;
                    var valLabel = card.Controls.Find("val_" + key, true).FirstOrDefault() as Label;
                    if (valLabel != null)
                    {
                        int v = stats.ContainsKey(key) ? stats[key] : 0;
                        valLabel.Text = v.ToString("N0");
                    }
                }
            }
        }
    }
}
