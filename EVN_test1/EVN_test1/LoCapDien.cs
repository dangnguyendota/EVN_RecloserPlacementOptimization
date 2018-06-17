using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVN_test1
{
    public class LoCapDien
    {
        private List<LoCap> location = new List<LoCap>();
        private List<string> loCapDien;
        private List<string> maDoiTuong;
        private List<string> maLienKet;
        private List<ToaDo> toaDoDaiDien;
        private List<string> soThuTu;
        private List<string> soHieu;
        private List<string> LCDnames = new List<string>();
        public LoCapDien(List<string> maDoiTuong, List<string> maLienKet, List<ToaDo> toaDoDaiDien, List<string> soThuTu, List<string> soHieu, List<string> loCapDien)
        {
            this.loCapDien = loCapDien;
            this.maDoiTuong = maDoiTuong;
            this.maLienKet = maLienKet;
            this.toaDoDaiDien = toaDoDaiDien;
            this.soThuTu = soThuTu;
            this.soHieu = soHieu;
            for(int i = 0; i < loCapDien.Count; i++)
            {
                string lo = loCapDien[i];
                string tmp = getName(lo);
                int index = Existed(tmp, location);
                if(index != -1)
                {
                    location[index].add(i);
                }
                else
                {
                    LoCap tmp_lc = new LoCap(tmp);
                    tmp_lc.add(i);
                    location.Add(tmp_lc);
                    LCDnames.Add(tmp);
          
                }
                
            }
        }

        private int Existed(string name, List<LoCap> loCap)
        {
            for(int i = 0; i < loCap.Count; i++)
            {
                if (name == loCap[i].getName())
                {
                    return i;
                }
            }
            return -1;
        }

        private string getName(string lo)
        {
            int tmp = 0;
            for(int i =0; i< lo.Length; i++)
            {
                if(lo[i] == '-' && tmp == 0)
                {
                    tmp = 1;
                }else if(lo[i] == '-' && tmp == 1)
                {
                    return lo.Substring(0, i);
                }
            }
            return lo;
        }

        public List<string> getLCDnames()
        {
            return LCDnames;
        }

        public List<string> gLoCapDien(List<string> LCDnames)
        {
            List<string> result = new List<string>();
            foreach (string LCDname in LCDnames)
            {
                int pos = LCDnames.IndexOf(LCDname);
                List<int> location_list = location[pos].getList();
                foreach (int i in location_list)
                {
                    result.Add(this.loCapDien[i]);
                }
            }
            return result;

        }
        public List<string> gMaDoiTuong(List<string> LCDnames)
        {
            List<string> result = new List<string>();
            foreach (string LCDname in LCDnames)
            {
                int pos = LCDnames.IndexOf(LCDname);
                List<int> location_list = location[pos].getList();
                foreach (int i in location_list)
                {
                    result.Add(this.maDoiTuong[i]);
                }
            }
            return result;
        }
        public List<string> gMaLienKet(List<string> LCDnames)
        {
            List<string> result = new List<string>();
            foreach (string LCDname in LCDnames)
            {
                int pos = LCDnames.IndexOf(LCDname);
                List<int> location_list = location[pos].getList();
                foreach (int i in location_list)
                {
                    result.Add(this.maLienKet[i]);
                }
            }
            return result;
        }
        public List<ToaDo> gToaDoDaiDien(List<string> LCDnames)
        {
            List<ToaDo> result = new List<ToaDo>();
            foreach (string LCDname in LCDnames)
            {
                int pos = LCDnames.IndexOf(LCDname);
                List<int> location_list = location[pos].getList();
                foreach (int i in location_list)
                {
                    result.Add(this.toaDoDaiDien[i]);
                }
            }
            return result;
        }
        public List<string> gSoThuTu(List<string> LCDnames)
        {
            List<string> result = new List<string>();
            foreach (string LCDname in LCDnames)
            {
                int pos = LCDnames.IndexOf(LCDname);
                List<int> location_list = location[pos].getList();
                foreach (int i in location_list)
                {
                    result.Add(this.soThuTu[i]);
                }
            }
            return result;
        }
        public List<string> gSoHieu(List<string> LCDnames)
        {
            List<string> result = new List<string>();
            foreach (string LCDname in LCDnames)
            {
                int pos = LCDnames.IndexOf(LCDname);
                List<int> location_list = location[pos].getList();
                foreach (int i in location_list)
                {
                    result.Add(this.soHieu[i]);
                }
            }
            return result;
        }
    }
}
