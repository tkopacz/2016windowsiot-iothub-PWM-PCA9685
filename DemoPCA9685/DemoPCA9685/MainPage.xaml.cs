﻿using Microsoft.IoT.Lightning.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Pwm;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DemoPCA9685
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private PwmController m_pwmController;
        private PwmPin m_servo0;
        private PwmPin m_servo1;
        private DispatcherTimer m_timer;

        public MainPage()
        {
            this.InitializeComponent();
            setup();
        }

        private async Task setup()
        {
            try
            {
                //PCA9685
                m_pwmController = (await PwmController.GetControllersAsync(LightningPwmProvider.GetPwmProvider()))[0];
                m_pwmController.SetDesiredFrequency(50); //For Servo
                m_servo0 = m_pwmController.OpenPin(0);
                m_servo1 = m_pwmController.OpenPin(1);

                m_timer = new DispatcherTimer();
                m_timer.Tick += M_timer_Tick;
                m_timer.Interval = TimeSpan.FromMilliseconds(500);
                m_timer.Start();
            } catch (Exception ex)
            {
                Debug.Write(ex.ToString());
                throw;
            }

        }

        const double STEP = 0.1;
        double m_duty0 = 0, m_duty1 = 1, m_dir0 = +STEP, m_dir1 = -STEP;
        const double MINDUTY = 0, MAXDUTY = 1;
        private void M_timer_Tick(object sender, object e)
        {
            Debug.WriteLine($"{m_duty0}, {m_duty1}");
            m_servo0.SetActiveDutyCyclePercentage(m_duty0);
            m_servo1.SetActiveDutyCyclePercentage(m_duty1);
            m_servo0.Start();
            m_servo1.Start();
            m_duty0 += m_dir0;
            m_duty1 += m_dir1;
            if (m_duty0 < MINDUTY) { m_duty0 = MINDUTY; m_dir0 = STEP; }
            if (m_duty0 > MAXDUTY) { m_duty0 = MAXDUTY; m_dir0 = -STEP; }
            if (m_duty1 < MINDUTY) { m_duty1 = MINDUTY; m_dir1 = STEP; }
            if (m_duty1 > MAXDUTY) { m_duty1 = MAXDUTY; m_dir1 = -STEP; }
        }
    }
}
