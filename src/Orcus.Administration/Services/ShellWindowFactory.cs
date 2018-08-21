﻿using Orcus.Administration.Library.Services;
using Orcus.Administration.Views;

namespace Orcus.Administration.Services
{
    public class ShellWindowFactory : IShellWindowFactory
    {
        public IShellWindow Create() => new WindowInstance(new ShellWindow());
    }
}