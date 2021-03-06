﻿using System;
using System.Drawing;
using WindowsAPI;
using ImageProcessing;
using System.Windows.Forms;

namespace WindowsHacks
{

    /// <summary>
    /// Creates an outline of a window.
    /// </summary>
    public static class TraceWindow
    {
        public static void Run(int thresholds)
        {
            if (thresholds < 1 || thresholds > 255) throw new Exception("Threshold must be 1-255.");

            IntPtr hWnd = OtherFunctions.GetFocusedWindow();

            System.Threading.Thread.Sleep(100);
            Window.SetFocused(hWnd);
            System.Threading.Thread.Sleep(100);
            Bitmap screenshot = Window.Screenshot(hWnd);

            if (thresholds <= 1) screenshot = Effect.Threshold(screenshot);
            else
            {
                int[] array = new int[thresholds];
                int count = 0;
                for (int j = 0; j < array.Length; j++)
                {
                    count += (255 / thresholds);
                    array[j] = count;
                }
                screenshot = Effect.Threshold(screenshot, array);
            }

            Mask mask = new Mask(hWnd, screenshot);
            mask.TransparencyKey = Color.White;
            System.Threading.Thread.Sleep(100);
            Window.Close(hWnd);
            Application.Run();
        }
    }
}
