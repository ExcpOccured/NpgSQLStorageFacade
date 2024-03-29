﻿using System;

namespace Npgsql.StorageFacade.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigSectionAttribute : Attribute
    {
        public ConfigSectionAttribute(string section)
        {
            Section = section ?? throw new ArgumentNullException(nameof(section));
        }
        
        public string Section { get; }
    }
}