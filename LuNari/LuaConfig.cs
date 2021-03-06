﻿/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2016,2017,2019  Denis Kuzmin < entry.reg@gmail.com > :: github.com/3F
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/

using System;
using net.r_eg.Conari.Core;

namespace net.r_eg.LuNari
{
    public struct LuaConfig: IConfig
    {
        /// <summary>
        /// Module (.dll, .exe, or address).
        /// </summary>
        public string Module
        {
            get;
            set;
        }

        /// <summary>
        /// To use `commit` methods for end calling.
        /// TODO:
        /// </summary>
        public bool TransactionStrategy
        {
            get;
            set;
        }

        /// <summary>
        /// To load library only when required.
        /// </summary>
        public bool LazyLoading
        {
            get;
            set;
        }

        /// <summary>
        /// To cache dynamic types.
        /// </summary>
        public bool CacheDLR
        {
            get;
            set;
        }

        /// <summary>
        /// Auto name-decoration to find entry points of exported proc.
        /// </summary>
        public bool Mangling
        {
            get;
            set;
        }

        /// <summary>
        /// https://github.com/3F/Conari/issues/15
        /// Windows will prevent new loading and return the same handle as for the first loaded module due to used reference count for each trying to load the same module (dll or exe).
        /// Actual new loading and its new handle is possible when reference count is less than 1.
        /// 
        /// Through Conari this means each decrementing when disposing is processed on implemented such as ConariL object.
        /// That is, each new instance will increase total reference count by +1 and each disposing will decrease it by -1.
        /// But it can produce the problem not only in multithreading but even between third processes.
        /// 
        /// This option will isolate module for a real new loading even if it was already loaded somewhere else.
        /// </summary>
        public bool IsolateLoadingOfModule
        {
            get;
            set;
        }

        public static explicit operator string(LuaConfig cfg) => cfg.Module;

        public static explicit operator LuaConfig(string lib) => new LuaConfig(lib);

        /// <param name="lib">Path to Lua library.</param>
        /// <param name="isolate">Initialize property {IsolateLoadingOfModule}.</param>
        public LuaConfig(string lib, bool isolate = false)
            : this()
        {
            Module      = lib;
            CacheDLR    = true;

            IsolateLoadingOfModule = isolate;
        }
    }
}
