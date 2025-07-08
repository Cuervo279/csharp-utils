/// <summary>
/// Altera a cor de fundo de todos os elementos cujo nome começa com "pnl", de forma recursiva.
/// Exemplo: SetCor(this, "pnl", SystemColors.ControlLight);
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

/// <summary>
/// Aplica um efeito de zoom suave enquanto o mouse está sobre o controle.
/// </summary>
public sealed class HoverZoomBehavior
        {
              private readonly Control _ctrl;
              private readonly System.Windows.Forms.Timer _timer;
              private readonly int _maxIncrease;
              private readonly int _step;
              private readonly Size _originalSize;
              private bool _zoomIn;

              public HoverZoomBehavior(
                  Control control,
                  int maxIncrease = 5,
                  int step = 2,
                  int intervalMs = 16)
                      {
                         _ctrl = control;
                         _maxIncrease = maxIncrease;
                         _step = step;
                         _originalSize = control.Size;

                         _timer = new System.Windows.Forms.Timer { Interval = intervalMs };
                         _timer.Tick += Tick;

                          control.MouseEnter += (_, __) => { _zoomIn = true; _timer.Start(); };
                          control.MouseLeave += (_, __) => { _zoomIn = false; _timer.Start(); };
                      }

      private void Tick(object? sender, EventArgs e)
              {
                  int w = _ctrl.Width + (_zoomIn ? _step : -_step);
                  int h = _ctrl.Height + (_zoomIn ? _step : -_step);
        
                  int minW = _originalSize.Width;
                  int maxW = _originalSize.Width + _maxIncrease;
                  int minH = _originalSize.Height;
                  int maxH = _originalSize.Height + _maxIncrease;
        
                  w = Math.Clamp(w, minW, maxW);
                  h = Math.Clamp(h, minH, maxH);
        
                  _ctrl.Left -= (w - _ctrl.Width) / 2;
                  _ctrl.Top -= (h - _ctrl.Height) / 2;
                  _ctrl.Size = new Size(w, h);

                  if ((_zoomIn && w == maxW) || (!_zoomIn && w == minW))
                      _timer.Stop();
              }
  }

  /// <summary>
  /// Conveniência: extensão para ativar o efeito 'HoverZoomBehavior' com uma linha.
  /// Exemplo de uso:  imgTheme.EnableHoverZoom();  padrão: +5 px, step 2
  ///                  btnX.EnableHoverZoom(8, 3);  exemplo com outro tamanho
  ///                  pictureBox1.EnableHoverZoom(maxIncrease: 7);
  /// </summary>
  public static class HoverZoomExtensions
  {
      public static void EnableHoverZoom(
          this Control control,
          int maxIncrease = 5,
          int step = 2,
          int intervalMs = 16)
      {
          if (control.Tag is HoverZoomBehavior) return;
          control.Tag = new HoverZoomBehavior(control, maxIncrease, step, intervalMs);
      }
  }
