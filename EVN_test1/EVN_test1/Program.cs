using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EVN_test1
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> maDoiTuong = new List<string>();
            List<string> maLienKet = new List<string>();
            List<ToaDo> toaDoDaiDien = new List<ToaDo>();
            List<string> soThuTu = new List<string>();
            List<string> soHieu = new List<string>();
            List<string> loCapDien = new List<string>();
            List<List<double>> toaDo = new List<List<double>>();
            using (StreamReader sr1 = new StreamReader(@"E:\PycharmProjects\EVN 2\soHieu.txt"))
            {
                string line;

                while ((line = sr1.ReadLine()) != null)
                {
                    soHieu.Add(line.Trim());
                }
            }
            using (StreamReader sr = new StreamReader(@"E:\PycharmProjects\EVN 2\maDoiTuong.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    maDoiTuong.Add(line.Trim());
                }
            }
            using (StreamReader sr = new StreamReader(@"E:\PycharmProjects\EVN 2\loCapDien.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    loCapDien.Add(line.Trim());
                }
            }
            using (StreamReader sr = new StreamReader(@"E:\PycharmProjects\EVN 2\maLienKet.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    maLienKet.Add(line.Trim());
                }
            }
            using (StreamReader sr = new StreamReader(@"E:\PycharmProjects\EVN 2\toaDoXDaiDien.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    List<double> temp = new List<double>();
                    temp.Add(double.Parse(line.Trim()));
                    toaDo.Add(temp);
                }
            }
            using (StreamReader sr = new StreamReader(@"E:\PycharmProjects\EVN 2\toaDoYDaiDien.txt"))
            {
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    toaDo[i].Add(double.Parse(line.Trim()));
                    i++;
                }
            }
            using (StreamReader sr = new StreamReader(@"E:\PycharmProjects\EVN 2\soThuTu.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    soThuTu.Add(line.Trim());
                }
            }
            for (int i = 0; i < maDoiTuong.Count; i++)
            {
                //Console.WriteLine(toaDo[i][0].ToString() + "|" + toaDo[i][1].ToString());
                toaDoDaiDien.Add(new EVN_test1.ToaDo(toaDo[i][0], toaDo[i][1]));
            }
            ViTriDat v = new ViTriDat(maDoiTuong, maLienKet, toaDoDaiDien, soThuTu, soHieu, loCapDien, 10, 30, 40);
            List<string> may_cat = v.layViTriDat(TYPE_OBJECT.MAY_CAT);
            List<string> dao_tu_dong = v.layViTriDat(TYPE_OBJECT.DAO_TU_DONG);
            List<string> den_bao = v.layViTriDat(TYPE_OBJECT.DEN_BAO);
            foreach (string i in may_cat)
            {
                Console.WriteLine("May cat:" + i);
            }
            foreach (string i in den_bao)
            {
                Console.WriteLine("Den bao:" + i);
            }
            foreach (string i in dao_tu_dong)
            {
                Console.WriteLine("Dao Tu Dong:" + i);
            }
        }
    }
}
