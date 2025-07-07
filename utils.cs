/// <summary>
/// Altera a cor de fundo de todos os elementos cujo nome começa com "pnl", de forma recursiva.
/// </summary>
/// <param name="pai">O controle pai (geralmente o formulário ou um container).</param>
/// <param name="tipo">Nome do elemento (geralmente uma sigla).</param>
/// <param name="corDesejada">A cor que será aplicada como fundo.</param>

private void SetCor(Control pai, string tipo, Color corDesejada)
        {
            foreach (Control ctl in pai.Controls)
            {
                if (ctl is Panel && ctl.Name.StartsWith(tipo, StringComparison.OrdinalIgnoreCase))
                    ctl.BackColor = corDesejada;

                if (ctl.HasChildren)
                    SetCor(ctl, tipo, corDesejada);
            }
        }
