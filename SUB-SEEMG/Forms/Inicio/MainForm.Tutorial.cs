using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SUB.Forms.Configuracoes;
using SUB.Forms.Livros;
using SUB.Forms.Relatorio;
using SUB.Forms.Usuario;
using SUB.Models;
using SUB.Utils;

namespace SUB.Forms.Inicio
{
    public partial class MainForm
    {
        private GuidedTourOverlay tutorialOverlay;
        private bool tutorialInicialAgendado;
        private readonly List<Form> formulariosCriadosPeloTutorial = new List<Form>();
        private enum TelaDoTutorial
        {
            Inicio,
            Pessoas,
            CadastroUsuario,
            EditarUsuario,
            Acervo,
            CadastroLivro,
            Emprestimos,
            EmprestimoRapido,
            Devolucao,
            Relatorios,
            Configuracao
        }

        private void AgendarTutorialInicial()
        {
            if (tutorialInicialAgendado)
                return;
            tutorialInicialAgendado = true;
            BeginInvoke(new Action(() =>
            {
                if (!IsDisposed && TutorialStateStore.DeveExibir(Sessao.UsuarioId))
                    IniciarTutorial();
            }));
        }

        private void MainForm_TutorialSolicitado(object sender, EventArgs e)
        {
            if (IsDisposed)
                return;
            BeginInvoke(new Action(() =>
            {
                if (!IsDisposed)
                    IniciarTutorial();
            }));
        }

        private void IniciarTutorial()
        {
            if (tutorialOverlay != null && !tutorialOverlay.IsDisposed)
                return;
            ReabilitarBotoesDeNavegacao();
            btnIn();
            DefinirNavegacaoAtiva(btnInicio);
            inicio?.DefinirVisibilidadeDoStatus(false);
            tutorialOverlay = new GuidedTourOverlay();
            tutorialOverlay.Completed += TutorialOverlay_Completed;
            tutorialOverlay.Skipped += TutorialOverlay_Skipped;
            Controls.Add(tutorialOverlay);
            tutorialOverlay.Bounds = ClientRectangle;
            tutorialOverlay.BringToFront();
            tutorialOverlay.Start(CriarEtapasTutorial(), corPrimaria, CapturarFundoDoTutorial, picLogoMenu.Image ?? picLogoMenu.BackgroundImage);
        }

        private void TutorialOverlay_Completed(object sender, EventArgs e)
        {
            TutorialStateStore.MarcarComoVisto(Sessao.UsuarioId);
            EncerrarTutorial(retornarAoInicio: true);
        }

        private void TutorialOverlay_Skipped(object sender, EventArgs e)
        {
            TutorialStateStore.MarcarComoVisto(Sessao.UsuarioId);
            EncerrarTutorial(retornarAoInicio: true);
        }

        private void EncerrarTutorial(bool retornarAoInicio)
        {
            if (tutorialOverlay == null)
                return;
            FecharFormulariosCriadosPeloTutorial();
            tutorialOverlay.Completed -= TutorialOverlay_Completed;
            tutorialOverlay.Skipped -= TutorialOverlay_Skipped;
            inicio?.DefinirVisibilidadeDoStatus(true);
            GarantirMenusDoTutorial(false, false);
            if (retornarAoInicio)
            {
                ReabilitarBotoesDeNavegacao();
                btnIn();
                DefinirNavegacaoAtiva(btnInicio);
            }

            Controls.Remove(tutorialOverlay);
            tutorialOverlay.Dispose();
            tutorialOverlay = null;
        }

        private List<TutorialStep> CriarEtapasTutorial()
        {
            return new List<TutorialStep>
            {
                new TutorialStep("INÍCIO", "Seu painel principal", "Acompanhe a operação da biblioteca e acesse os recursos principais pelo menu lateral.", () => btnInicio, () => PrepararTelaDoTutorial(TelaDoTutorial.Inicio, false, false)),
                new TutorialStep("PESSOAS", "Pessoas", "Use esta seção para consultar, cadastrar e editar os usuários atendidos pela biblioteca.", () => btnUsuario, () => PrepararTelaDoTutorial(TelaDoTutorial.Pessoas, true, false)),
                new TutorialStep("PESSOAS", "Consultar usuários", "Encontre usuários cadastrados e consulte suas informações antes de iniciar uma operação.", () => btnUser, () => PrepararTelaDoTutorial(TelaDoTutorial.Pessoas, true, false)),
                new TutorialStep("PESSOAS", "Cadastrar usuário", "Inclua novos usuários para que eles possam utilizar os serviços da biblioteca.", () => btnUserCad, () => PrepararTelaDoTutorial(TelaDoTutorial.CadastroUsuario, true, false)),
                new TutorialStep("PESSOAS", "Editar usuário", "Atualize informações de usuários já cadastrados sempre que for necessário.", () => btnUserEdit, () => PrepararTelaDoTutorial(TelaDoTutorial.EditarUsuario, true, false)),
                new TutorialStep("ACERVO", "Acervo", "Aqui ficam a consulta do acervo e o cadastro dos livros didáticos da instituição.", () => btnLivro, () => PrepararTelaDoTutorial(TelaDoTutorial.Acervo, false, true)),
                new TutorialStep("ACERVO", "Consultar acervo", "Consulte a disponibilidade e os dados dos livros antes de registrar uma operação.", () => btnLivros, () => PrepararTelaDoTutorial(TelaDoTutorial.Acervo, false, true)),
                new TutorialStep("ACERVO", "Cadastrar livro", "Cadastre novos livros para deixá-los disponíveis para empréstimos.", () => btnLivroCad, () => PrepararTelaDoTutorial(TelaDoTutorial.CadastroLivro, false, true)),
                new TutorialStep("EMPRÉSTIMOS", "Registrar empréstimo", "Registre empréstimos com todos os dados necessários para acompanhar a devolução corretamente.", () => btnEmprestimo, () => PrepararTelaDoTutorial(TelaDoTutorial.Emprestimos, false, true)),
                new TutorialStep("EMPRÉSTIMOS", "Empréstimo rápido", "Use esta opção para registrar rapidamente os empréstimos mais frequentes da rotina.", () => btnEmprestimoRap, () => PrepararTelaDoTutorial(TelaDoTutorial.EmprestimoRapido, false, true)),
                new TutorialStep("EMPRÉSTIMOS", "Registrar devolução", "Registre a devolução assim que o livro retornar para manter o acervo atualizado.", () => btnDev, () => PrepararTelaDoTutorial(TelaDoTutorial.Devolucao, false, true)),
                new TutorialStep("RELATÓRIOS", "Relatórios", "Consulte o histórico da operação, aplique filtros e exporte as informações que precisar.", () => btnRel, () => PrepararTelaDoTutorial(TelaDoTutorial.Relatorios, false, false)),
                new TutorialStep("CONFIGURAÇÃO", "Configuração", "Altere o tema, mantenha os dados da instituição atualizados e use “Rever tutorial” quando quiser rever esta visão geral.", () => btnConfiguracao, () => PrepararTelaDoTutorial(TelaDoTutorial.Configuracao, false, false))
            };
        }

        private void PrepararTelaDoTutorial(TelaDoTutorial tela, bool pessoasExpandido, bool acervoExpandido)
        {
            ReabilitarBotoesDeNavegacao();
            switch (tela)
            {
                case TelaDoTutorial.Inicio:
                    btnIn();
                    DefinirNavegacaoAtiva(btnInicio);
                    break;
                case TelaDoTutorial.Pessoas:
                    if (usuario == null || usuario.IsDisposed)
                    {
                        usuario = new UsuarioForm();
                        usuario.FormClosed += Usuario_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(usuario);
                    }

                    OpenChild(usuario, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnUsuario);
                    break;
                case TelaDoTutorial.CadastroUsuario:
                    if (usuarioCad == null || usuarioCad.IsDisposed)
                    {
                        usuarioCad = new CadUsuario();
                        usuarioCad.FormClosed += UsuarioCad_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(usuarioCad);
                    }

                    OpenChild(usuarioCad, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnUsuario);
                    break;
                case TelaDoTutorial.EditarUsuario:
                    if (usuarioEdit == null || usuarioEdit.IsDisposed)
                    {
                        usuarioEdit = new EditarUsuarioForm();
                        usuarioEdit.FormClosed += UsuarioEdit_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(usuarioEdit);
                    }

                    OpenChild(usuarioEdit, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnUsuario);
                    break;
                case TelaDoTutorial.Acervo:
                    if (livros == null || livros.IsDisposed)
                    {
                        livros = new LivrosForm();
                        livros.FormClosed += Livros_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(livros);
                    }

                    OpenChild(livros, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnLivro);
                    break;
                case TelaDoTutorial.CadastroLivro:
                    if (cadastroLivro == null || cadastroLivro.IsDisposed)
                    {
                        cadastroLivro = new CadastroLivroForm();
                        cadastroLivro.FormClosed += CadastroLivro_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(cadastroLivro);
                    }

                    OpenChild(cadastroLivro, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnLivro);
                    break;
                case TelaDoTutorial.Emprestimos:
                    if (emprestimo == null || emprestimo.IsDisposed)
                    {
                        emprestimo = new EmprestimoForm();
                        emprestimo.FormClosed += Emprestimo_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(emprestimo);
                    }

                    OpenChild(emprestimo, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnLivro);
                    break;
                case TelaDoTutorial.EmprestimoRapido:
                    if (emprestimoRap == null || emprestimoRap.IsDisposed)
                    {
                        emprestimoRap = new EmprestimoRapidoForm();
                        emprestimoRap.FormClosed += EmprestimoRap_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(emprestimoRap);
                    }

                    OpenChild(emprestimoRap, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnLivro);
                    break;
                case TelaDoTutorial.Devolucao:
                    if (devolução == null || devolução.IsDisposed)
                    {
                        devolução = new DevoluçãoForm();
                        devolução.FormClosed += Devolução_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(devolução);
                    }

                    OpenChild(devolução, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnLivro);
                    break;
                case TelaDoTutorial.Relatorios:
                    if (rel == null || rel.IsDisposed)
                    {
                        rel = new RelForm();
                        rel.FormClosed += Relatorios_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(rel);
                    }

                    OpenChild(rel, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnRel);
                    break;
                case TelaDoTutorial.Configuracao:
                    if (configuracao == null || configuracao.IsDisposed)
                    {
                        configuracao = new ConfiguracaoForm();
                        configuracao.FormClosed += Configuracao_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(configuracao);
                    }

                    OpenChild(configuracao, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnConfiguracao);
                    break;
            }

            GarantirMenusDoTutorial(pessoasExpandido, acervoExpandido);
            PerformLayout();
            Update();
        }

        private void RegistrarFormularioCriadoPeloTutorial(Form formulario)
        {
            if (formulario != null && !formulariosCriadosPeloTutorial.Contains(formulario))
                formulariosCriadosPeloTutorial.Add(formulario);
        }

        private void FecharFormulariosCriadosPeloTutorial()
        {
            foreach (Form formulario in formulariosCriadosPeloTutorial.ToArray())
            {
                if (!formulario.IsDisposed)
                    formulario.Close();
            }

            formulariosCriadosPeloTutorial.Clear();
            activeChild = null;
        }

        private Bitmap CapturarFundoDoTutorial()
        {
            if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
                return null;
            Bitmap snapshot = new Bitmap(ClientSize.Width, ClientSize.Height);
            using (Graphics graphics = Graphics.FromImage(snapshot))
            {
                graphics.Clear(BackColor);
                DesenharControleNoSnapshot(graphics, activeChild);
                DesenharControleNoSnapshot(graphics, menu);
                DesenharControleNoSnapshot(graphics, panelControl);
            }

            return snapshot;
        }

        private void DesenharControleNoSnapshot(Graphics graphics, Control control)
        {
            if (control == null || control.IsDisposed || !control.Visible || control.Width <= 0 || control.Height <= 0)
                return;
            using (Bitmap image = new Bitmap(control.Width, control.Height))
            {
                control.DrawToBitmap(image, control.ClientRectangle);
                Point location = RectangleToClient(control.RectangleToScreen(control.ClientRectangle)).Location;
                graphics.DrawImageUnscaled(image, location);
            }
        }

        private void GarantirMenusDoTutorial(bool pessoasExpandido, bool acervoExpandido)
        {
            userTransition.Stop();
            livroTransition.Stop();
            menuAnimating = false;
            userContainer.Height = pessoasExpandido ? (userContainer.Tag is int alturaPessoas ? alturaPessoas : 200) : 56;
            livroContainer.Height = acervoExpandido ? (livroContainer.Tag is int alturaAcervo ? alturaAcervo : 296) : 56;
            userExpand = pessoasExpandido;
            livroExpand = acervoExpandido;
            menu.PerformLayout();
        }
    }
}
