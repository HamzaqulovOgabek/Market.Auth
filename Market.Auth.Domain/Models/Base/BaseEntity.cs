using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Auth.Domain.Models;

public class BaseEntity<TId>  where TId : struct
{
    [Key]
    public TId Id{ get; set; }
}
