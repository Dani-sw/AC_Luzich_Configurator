using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

public static class GameInputHelper
{
    // Importazioni Win32 API
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

    [DllImport("user32.dll")]
    private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern bool IsWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    // Costanti
    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    private const byte VK_ESCAPE = 0x1B;
    private const uint KEYEVENTF_KEYUP = 0x0002;

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    /// <summary>
    /// Invia ESC e fa click del mouse in una posizione specifica nella finestra del gioco
    /// </summary>
    /// <param name="relativeX">Coordinata X relativa alla finestra (es. 100 pixel dal bordo sinistro)</param>
    /// <param name="relativeY">Coordinata Y relativa alla finestra (es. 50 pixel dall'alto)</param>
    /// <returns>True se l'operazione è riuscita, False altrimenti</returns>
    public static bool SendEscAndClickToGame(int relativeX, int relativeY)
    {
        try
        {
            // 1. Trova la finestra
            IntPtr hWnd = FindWindow("acsW", "Assetto Corsa");

            if (hWnd == IntPtr.Zero)
            {
                Console.WriteLine("Errore: Finestra 'Assetto Corsa' non trovata.");
                return false;
            }

            // Verifica che la finestra sia valida
            if (!IsWindow(hWnd))
            {
                Console.WriteLine("Errore: Handle finestra non valido.");
                return false;
            }

            // 2. Porta la finestra in primo piano
            bool foregroundResult = SetForegroundWindow(hWnd);
            if (!foregroundResult)
            {
                Console.WriteLine("Attenzione: Impossibile portare la finestra in primo piano.");
            }

            // Attendi che la finestra sia effettivamente in primo piano
            Thread.Sleep(200);

            // Verifica che la finestra sia in foreground
            IntPtr foregroundWindow = GetForegroundWindow();
            if (foregroundWindow != hWnd)
            {
                Console.WriteLine("Attenzione: La finestra potrebbe non essere in primo piano.");
            }

            // 3. Invia il tasto ESC
            SendEscapeKey();
            Console.WriteLine("Tasto ESC inviato.");

            // Attendi che il gioco processi ESC
            Thread.Sleep(300);

            // 4. Ottieni le coordinate della finestra
            RECT windowRect;
            if (!GetWindowRect(hWnd, out windowRect))
            {
                Console.WriteLine("Errore: Impossibile ottenere le coordinate della finestra.");
                return false;
            }

            // 5. Calcola le coordinate assolute dello schermo
            int screenX = windowRect.Left + relativeX;
            int screenY = windowRect.Top + relativeY;

            // Verifica che le coordinate siano all'interno della finestra
            if (screenX < windowRect.Left || screenX > windowRect.Right ||
                screenY < windowRect.Top || screenY > windowRect.Bottom)
            {
                Console.WriteLine($"Attenzione: Coordinate ({relativeX}, {relativeY}) fuori dai limiti della finestra.");
            }

            // 6. Sposta il cursore e fai click
            PerformMouseClick(screenX, screenY);
            Console.WriteLine($"Click del mouse eseguito alla posizione: ({screenX}, {screenY})");

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore durante l'operazione: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Invia il tasto ESC
    /// </summary>
    private static void SendEscapeKey()
    {
        // Premi ESC
        keybd_event(VK_ESCAPE, 0, 0, 0);
        Thread.Sleep(50);
        // Rilascia ESC
        keybd_event(VK_ESCAPE, 0, KEYEVENTF_KEYUP, 0);
    }

    /// <summary>
    /// Esegue un click del mouse in una posizione specifica
    /// </summary>
    private static void PerformMouseClick(int x, int y)
    {
        // Sposta il cursore
        SetCursorPos(x, y);
        Thread.Sleep(100);

        // Click sinistro: premi e rilascia
        mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
        Thread.Sleep(50);
        mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
    }

    /// <summary>
    /// Versione alternativa che usa coordinate percentuali (0.0 - 1.0)
    /// </summary>
    public static bool SendEscAndClickToGamePercent(double percentX, double percentY)
    {
        try
        {
            IntPtr hWnd = FindWindow("acsW", "Assetto Corsa");

            if (hWnd == IntPtr.Zero)
            {
                Console.WriteLine("Errore: Finestra non trovata.");
                return false;
            }

            RECT windowRect;
            if (!GetWindowRect(hWnd, out windowRect))
            {
                return false;
            }

            int width = windowRect.Right - windowRect.Left;
            int height = windowRect.Bottom - windowRect.Top;

            int relativeX = (int)(width * percentX);
            int relativeY = (int)(height * percentY);

            return SendEscAndClickToGame(relativeX, relativeY);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore: {ex.Message}");
            return false;
        }
    }
}



public class GameController
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        public uint type;
        public InputUnion u;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct InputUnion
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    private const uint INPUT_MOUSE = 0;
    private const uint WM_KEYDOWN = 0x0100;
    private const uint WM_KEYUP = 0x0101;
    private const uint MOUSEEVENTF_MOVE = 0x0001;
    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    private const int VK_ESCAPE = 0x1B;


    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("User32.dll")]
    static extern int SetForegroundWindow(IntPtr point);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    private static extern int ShowCursor(bool bShow);


    [DllImport("user32.dll")]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);




    public static bool SendEscAndClick(int targetX, int targetY)
    {
        IntPtr hWnd = FindWindow("acsW", "Assetto Corsa");
        if (hWnd == IntPtr.Zero)
            return false;

        SetForegroundWindow(hWnd);
        Thread.Sleep(100);

        // 1. "Sveglia" il cursore muovendolo un po'
        GetCursorPos(out POINT currentPos);

        // Muovi il mouse avanti e indietro per farlo apparire
        SetCursorPos(currentPos.X + 10, currentPos.Y + 10);
        Thread.Sleep(30);
        SetCursorPos(currentPos.X, currentPos.Y);
        Thread.Sleep(30);

        // Forza la visualizzazione del cursore
        ShowCursor(true);
        Thread.Sleep(50);

        // 2. Invia ESC
        PostMessage(hWnd, WM_KEYDOWN, (IntPtr)VK_ESCAPE, IntPtr.Zero);
        Thread.Sleep(200);
        PostMessage(hWnd, WM_KEYUP, (IntPtr)VK_ESCAPE, IntPtr.Zero);
        Thread.Sleep(400);

        // 3. Muovi il mouse alla posizione target
        //Importante la patch di AC nasconde il mouse ed il click funziona male , bisogna spostarlo e farlo vedere prima
        SetCursorPos(targetX, targetY);
        Thread.Sleep(150);

        // 4. Click
        SendMouseClick();

        return true;
    }

    private static void SendMouseClick()
    {
        INPUT[] inputs = new INPUT[2];

        inputs[0].type = INPUT_MOUSE;
        inputs[0].u.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

        inputs[1].type = INPUT_MOUSE;
        inputs[1].u.mi.dwFlags = MOUSEEVENTF_LEFTUP;

        SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT)));
    }
}
