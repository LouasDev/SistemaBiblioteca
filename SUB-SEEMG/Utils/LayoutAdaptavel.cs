using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace SUB.Utils
{
    // As telas internas foram desenhadas para 1280×845. Em monitores de 1366×768, comuns nas escolas,
    // a ancoragem do WinForms subia os botões presos embaixo para cima dos campos e os filtros de
    // largura fixa saíam pela borda direita. Aqui o painel principal da tela passa a ser disposto assim:
    // - na altura nada se comprime abaixo do desenho; só as grades (presas em cima e embaixo) encolhem;
    // - na largura vale a ancoragem do designer, mas se ela estourar a borda ou sobrepor controles,
    //   a faixa é comprimida na horizontal (posições e larguras, sem mexer nas fontes);
    // - se ainda faltar altura, o painel ganha rolagem.
    // Em tamanhos iguais ou maiores que o desenho o resultado é o mesmo da ancoragem original.
    internal static class LayoutAdaptavel
    {
        private const int Margem = 16;
        private const int AlturaMinimaGrade = 160;

        private sealed class Item
        {
            public Control Controle;
            public Rectangle Desenho;
            public AnchorStyles Ancora;
            public List<KeyValuePair<Control, Rectangle>> Filhos;
        }

        private sealed class Disposicao
        {
            public Size Desenho;
            public readonly List<Item> Itens = new List<Item>();
            public bool Comprimida;
            public bool Dispondo;
        }

        private static readonly ConditionalWeakTable<Control, Disposicao> Disposicoes =
            new ConditionalWeakTable<Control, Disposicao>();

        // Chamar logo após InitializeComponent, com a tela ainda no tamanho do designer.
        public static void Aplicar(Form tela)
        {
            var painel = tela.Controls.OfType<Panel>().FirstOrDefault(p => p.Dock == DockStyle.Fill);
            if (painel == null || painel.AutoScroll)
                return;
            Disposicao existente;
            if (Disposicoes.TryGetValue(painel, out existente))
                return;

            var disposicao = new Disposicao { Desenho = painel.ClientSize };
            foreach (Control controle in painel.Controls)
            {
                if (controle.Dock != DockStyle.None)
                    continue;
                var item = new Item { Controle = controle, Desenho = controle.Bounds, Ancora = controle.Anchor };
                if (controle is Panel || controle is GroupBox)
                {
                    item.Filhos = controle.Controls.Cast<Control>()
                        .Where(f => f.Dock == DockStyle.None)
                        .Select(f => new KeyValuePair<Control, Rectangle>(f, f.Bounds))
                        .ToList();
                }
                disposicao.Itens.Add(item);
                controle.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }

            Disposicoes.Add(painel, disposicao);
            painel.Resize += (s, e) => Dispor(painel, disposicao);
        }

        private static void Dispor(Panel painel, Disposicao d)
        {
            if (d.Dispondo || d.Itens.Count == 0 || painel.Width <= 0 || painel.Height <= 0)
                return;

            d.Dispondo = true;
            painel.SuspendLayout();
            try
            {
                int dH = painel.Height - d.Desenho.Height;
                var posicoes = d.Itens.ToDictionary(i => i, i => Vertical(i, dH));

                // As listas de sugestão são flutuantes e ficam fora das contas de espaço.
                var solidos = d.Itens.Where(i => !(i.Controle is ListBox)).ToList();
                int fundo = solidos.Max(i => posicoes[i].Bottom) + Margem;
                bool rolagem = fundo > painel.Height;
                int largura = painel.Width - (rolagem ? SystemInformation.VerticalScrollBarWidth : 0);
                int dW = largura - d.Desenho.Width;

                foreach (var item in d.Itens)
                {
                    var horizontal = Horizontal(item, dW);
                    posicoes[item] = new Rectangle(horizontal.X, posicoes[item].Y, horizontal.Width, posicoes[item].Height);
                }

                float fator = 1f;
                if (dW < 0 && Conflita(d, solidos, posicoes, largura))
                    fator = Comprimir(d, solidos, posicoes, largura);

                foreach (var item in d.Itens)
                {
                    var r = posicoes[item];
                    item.Controle.SetBounds(r.X + painel.AutoScrollPosition.X, r.Y + painel.AutoScrollPosition.Y, r.Width, r.Height);
                    if (item.Filhos != null && (fator < 1f || d.Comprimida) && !Estica(item))
                    {
                        foreach (var filho in item.Filhos)
                        {
                            // A data ocupa a largura toda do campo; estreitá-la cortaria o ano.
                            var f = filho.Value;
                            int larguraFilho = filho.Key is RoundedDatePicker ? f.Width : Math.Max(1, (int)Math.Round(f.Width * fator));
                            filho.Key.SetBounds((int)Math.Round(f.X * fator), f.Y, larguraFilho, f.Height);
                        }
                    }
                }
                d.Comprimida = fator < 1f;

                painel.AutoScrollMinSize = rolagem ? new Size(0, fundo) : Size.Empty;
                painel.AutoScroll = rolagem;
            }
            finally
            {
                painel.ResumeLayout();
                d.Dispondo = false;
            }
        }

        private static bool Estica(Item item)
        {
            return (item.Ancora & AnchorStyles.Left) != 0 && (item.Ancora & AnchorStyles.Right) != 0;
        }

        // Mesmo arredondamento da ancoragem do WinForms para controles centralizados (meio pixel sobe).
        private static int Metade(int valor)
        {
            return (int)Math.Round(valor / 2.0, MidpointRounding.AwayFromZero);
        }

        private static Rectangle Vertical(Item item, int dH)
        {
            var r = item.Desenho;
            bool topo = (item.Ancora & AnchorStyles.Top) != 0;
            bool base_ = (item.Ancora & AnchorStyles.Bottom) != 0;
            if (topo && base_)
                return new Rectangle(r.X, r.Y, r.Width, Math.Max(Math.Min(r.Height, AlturaMinimaGrade), r.Height + dH));

            int sobra = Math.Max(0, dH);
            if (base_)
                return new Rectangle(r.X, r.Y + sobra, r.Width, r.Height);
            if (!topo)
                return new Rectangle(r.X, r.Y + Metade(sobra), r.Width, r.Height);
            return r;
        }

        private static Rectangle Horizontal(Item item, int dW)
        {
            var r = item.Desenho;
            bool esquerda = (item.Ancora & AnchorStyles.Left) != 0;
            bool direita = (item.Ancora & AnchorStyles.Right) != 0;
            if (esquerda && direita)
                return new Rectangle(r.X, r.Y, Math.Max(r.Width / 2, r.Width + dW), r.Height);
            if (direita)
                return new Rectangle(r.X + dW, r.Y, r.Width, r.Height);
            if (!esquerda)
                return new Rectangle(r.X + Metade(dW), r.Y, r.Width, r.Height);
            return r;
        }

        // A ancoragem normal falha quando algo passa da borda direita ou encosta em outro controle
        // que não encostava no desenho (ex.: o painel "Ações" centralizado sobre o campo de código).
        private static bool Conflita(Disposicao d, List<Item> solidos, Dictionary<Item, Rectangle> posicoes, int largura)
        {
            foreach (var item in solidos)
            {
                if (!Estica(item) && posicoes[item].Right > largura - Margem / 2 && item.Desenho.Right <= d.Desenho.Width)
                    return true;
            }

            for (int a = 0; a < solidos.Count; a++)
            {
                for (int b = a + 1; b < solidos.Count; b++)
                {
                    if (posicoes[solidos[a]].IntersectsWith(posicoes[solidos[b]]) &&
                        !solidos[a].Desenho.IntersectsWith(solidos[b].Desenho))
                        return true;
                }
            }
            return false;
        }

        // Volta às posições do desenho e as encolhe na horizontal a partir da margem esquerda
        // usada pelo título, até caber na largura disponível.
        private static float Comprimir(Disposicao d, List<Item> solidos, Dictionary<Item, Rectangle> posicoes, int largura)
        {
            var fixos = solidos.Where(i => !Estica(i)).ToList();
            if (fixos.Count == 0)
                return 1f;

            int esquerda = fixos.Min(i => i.Desenho.Left);
            int direita = fixos.Max(i => i.Desenho.Right);
            float fator = Math.Min(1f, (largura - Margem - esquerda) / (float)Math.Max(1, direita - esquerda));

            foreach (var item in d.Itens.Where(i => !Estica(i)))
            {
                int x = esquerda + (int)Math.Round((item.Desenho.Left - esquerda) * fator);
                int w = Math.Max(1, (int)Math.Round(item.Desenho.Width * fator));
                posicoes[item] = new Rectangle(x, posicoes[item].Y, w, posicoes[item].Height);
            }
            return fator;
        }
    }
}
