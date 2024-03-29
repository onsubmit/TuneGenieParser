﻿//-----------------------------------------------------------------------
// <copyright file="Tests.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TuneGenieParser;

    /// <summary>
    /// Unit tests
    /// </summary>
    [TestClass]
    public class Tests
    {
        /// <summary>
        /// Basic unit test
        /// </summary>
        [TestMethod]
        public void GetLastHour()
        {
            Parser p = new Parser("kndd");
            NowPlaying nowPlaying = p.GetLastHour(out string response);
            Console.WriteLine(response);
        }
    }
}
