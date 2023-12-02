// Copyright (c) 2023, Brian Parker. All Rights Reserved.
// SomeData.cs licensed under the MIT License.

using System.ComponentModel.DataAnnotations;

namespace MultiTenantDbContext.Models
{
    public class SomeData
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(length: 450)]
        public required string Name { get; set; }
    }
}
