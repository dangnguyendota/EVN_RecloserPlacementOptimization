using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EVN_test1
{
    class Program
    {
        static List<List<string>> Read(string path)
        {
            List<List<string>> result = new List<List<string>>();
            List<string> vtMC = new List<string>();
            List<string> vtDTD = new List<string>();
            List<string> vtDB = new List<string>();
            List<string> input = new List<string>();
            using (StreamReader ip = new StreamReader(path))
            {
                string line;

                while ((line = ip.ReadLine()) != null)
                {
                    input.Add(line.Trim());
                }
            }
            foreach(string i in input)
            {
                Console.WriteLine(i);
            }
            string mc = "";
            string dtd = "";
            string db = "";
            int flag = 0;
            bool r = false;
            foreach(char i in input[0])
            {
                if(i == ']')
                {
                    r = false;
                } else if(i == '[')
                {
                    flag += 1;
                    r = true;
                }
                else if(r == true)
                {
                    if(flag == 1)
                    {
                        mc += i;
                    }else if(flag == 2)
                    {
                        dtd += i;
                    }else if(flag == 3)
                    {
                        db += i;
                    }
                }
            }
            vtMC = Split(mc);
            vtDTD = Split(dtd);
            vtDB = Split(db);
            result.Add(vtMC);
            result.Add(vtDTD);
            result.Add(vtDB);
            return result;
        }
        static List<string> Split(string list)
        {
            List<string> result = new List<string>();
            list = list.Replace("\",\"", "\"");
            string current = "";
            foreach(char i in list)
            {
                if(i == '\"' && current != "")
                {
                    result.Add(current.Trim());
                    current = "";
                }
                else if (i != '\"')
                {
                    current += i;
                }
            }
            return result ;
        }
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
            Console.WriteLine("Load data complete!");
            ////////////////////// Tach Doi Tuong ra ///////////////////////////////////
            //////////////////// Example ////////////////////////////////////////////
            LoCapDien LCdatas = new LoCapDien(maDoiTuong, maLienKet, toaDoDaiDien, soThuTu, soHieu, loCapDien);
            List<string> names = LCdatas.getLCDnames(); // Tra ve danh sach cac ten Lo Cap Dien.
            List<string> selected_name = new List<string>(); // Cac lo cap dien duoc chon
            for(int i = 0; i<40; i++)
            {
                selected_name.Add(names[i]);
            }
            ///////////////////////////////////////////////////////////////////////
            ViTriDat v = new ViTriDat(LCdatas.gMaDoiTuong(selected_name), LCdatas.gMaLienKet(selected_name), LCdatas.gToaDoDaiDien(selected_name), LCdatas.gSoThuTu(selected_name), LCdatas.gSoHieu(selected_name), LCdatas.gLoCapDien(selected_name));
            v.TimViTriThietBi(0, 0, 0); // Tim vi tri cac thiet bi trong cac lo cap dien da duoc chon 
            Console.WriteLine("F : "+v.F_tong().ToString()); // v.F_tong() lay F nho nhat
            //////////////////Tinh F cua nhung thiet bi nguoi dung dat vao////////////////////////////////
            List<List<string>> vtThietBi = Read("E:\\C#project\\EVN_code\\EVN_RecloserPlacementOptimization\\EVN_test1\\EVN_test1\\bin\\Debug\\Input.txt");
            List<string> vtMC = vtThietBi[0];
            List<string> vtDTD = vtThietBi[1];
            List<string> vtDB = vtThietBi[2];
            Console.WriteLine("F nguoi dung nhap vao: " + v.getF(vtMC, vtDTD, vtDB).ToString());
            ChiPhi cp = v.TimSoLuongThietBi(Setting.cpDT, Setting.Pr, Setting.Ps, Setting.Pf);
            Console.WriteLine("Chi phi nho nhat: " + cp.getF().ToString()+
                ", Voi so luong MC:"+cp.getSL_MC().ToString()+
                ",DTD :"+cp.getSL_DTD().ToString()+
                ",DB :"+cp.getSL_DB().ToString());
            
            /////////////////////////////////////////////////////////////////
            Console.WriteLine("Run complete!");
            List<string> may_cat = v.layViTriDat(TYPE_OBJECT.MAY_CAT);
            List<string> dao_tu_dong = v.layViTriDat(TYPE_OBJECT.DAO_TU_DONG);
            List<string> den_bao = v.layViTriDat(TYPE_OBJECT.DEN_BAO);
            Console.WriteLine("MC:");
            foreach (string i in may_cat)
            {
                //Console.WriteLine("May cat:" + i);
                Console.Write("\""+i + "\",");
            }
            Console.WriteLine();
            Console.WriteLine("DB :");
            foreach (string i in den_bao)
            {
                //Console.WriteLine("Den bao:" + i);
                Console.Write("\"" + i + "\",");
            }
            Console.WriteLine();
            Console.WriteLine("DTD :");
            foreach (string i in dao_tu_dong)
            {
                //Console.WriteLine("Dao Tu Dong:" + i);
                Console.Write("\"" + i + "\",");
            }
        }
    }
}
