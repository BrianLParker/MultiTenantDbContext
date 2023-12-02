// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// LocalConfiguration.cs licensed under the MIT License.

namespace MultiTenantDbContext.Models
{
    public class LocalConfiguration
    {
        public string[] Tenants { get; set; }
        public Dictionary<string, string> ConnectionStrings { get; set; }
    }
}
