using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebShopWebApi.DTOs;

public class LogCreateDto
{
    public string? Level { get; set; }
    public string? Message { get; set; }
}
