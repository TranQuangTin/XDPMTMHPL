using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using DAO;

namespace BUS
{
    public class ThongKeBUS
    {
        public List<DoiTuongThongKe> ThongKe(DateTime dt1, DateTime dt2)
        {
            ThongKeDAO tkd=new ThongKeDAO();
            return tkd.ThongKe(dt1, dt2);
        }
        public List<DoiTuongThongKe> BieuDo(DateTime dt1, DateTime dt2)
        {
            ThongKeDAO tkd = new ThongKeDAO();
            return tkd.BieuDo(dt1, dt2);
        }
    }
}
