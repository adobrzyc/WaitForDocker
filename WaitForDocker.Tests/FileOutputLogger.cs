﻿using System;
using WaitForDocker.Logger;

namespace WaitForDocker.Tests
{
    public class FileOutputLogger : ILogger
    {
        public void Log(string message)
        {
            using (var writer = new System.IO.StreamWriter(Console.OpenStandardOutput()))
                writer.WriteLine(message);
        }
    }
}