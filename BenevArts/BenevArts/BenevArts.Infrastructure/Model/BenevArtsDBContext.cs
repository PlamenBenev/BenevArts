using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenevArts.Infrastructure.Model
{
    public class BenevArtsDBContext : DbContext
    {
        public BenevArtsDBContext(DbContextOptions<BenevArtsDBContext> options)
            : base (options)
        {
            
        }
    }
}
