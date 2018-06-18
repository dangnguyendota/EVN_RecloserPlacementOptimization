﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVN_test1
{
    public enum TYPE_OBJECT { MAY_CAT = 1, DAO_TU_DONG = 2, DEN_BAO }
    public class ViTriDat
    {
        List<string> maDoiTuong;
        List<string> maLienKet;
        List<ToaDo> toaDoDaiDien;
        List<string> soThuTu;
        List<string> soHieu;
        List<string> loCapDien;
        List<String> tapMC = new List<string>();
        List<String> tapDTD = new List<string>();
        List<String> tapDB = new List<string>();
        List<double[]> tapMC_toaDo = new List<double[]>();
        List<double[]> tapDTD_toaDo = new List<double[]>();
        List<double[]> tapDB_toaDo = new List<double[]>();
        List<SearchPosition> search = new List<SearchPosition>();
        ChuyenDoiDuLieu c;
        TimCotLanCan a;
        List<List<int>> parent;
        List<List<double>> d;
        List<List<double>> w;
        List<List<int>> l;
        private double current_F = 0.0;
        public ViTriDat(List<string> maDoiTuong, List<string> maLienKet, List<ToaDo> toaDoDaiDien, List<string> soThuTu, List<string> soHieu, List<string> loCapDien)
        {
            this.maDoiTuong = maDoiTuong;
            this.maLienKet = maLienKet;
            this.toaDoDaiDien = toaDoDaiDien;
            this.soThuTu = soThuTu;
            this.soHieu = soHieu;
            this.loCapDien = loCapDien;
            a = new TimCotLanCan(maDoiTuong, maLienKet, toaDoDaiDien, soThuTu, soHieu, loCapDien);
            a.chuyenDoi();
            c = new ChuyenDoiDuLieu(a.layCotLienKet(), a.layCotGoc(), a.soCot, maDoiTuong, soHieu, toaDoDaiDien);
            parent = c.parent;
            d = c.d;
            w = c.w;
            l = c.l;
            chuanHoa(parent, d, w, l);
            for (int i = 0; i < parent.Count; i++)
            {
                search.Add(new SearchPosition(parent[i], d[i], w[i], l[i]));
            }
            
        }
        private int countOf(List<int> parent, int i)
        {
            int count = 0;
            foreach (int j in parent)
            {
                if (j == i)
                {
                    count++;
                }
            }
            return count;
        }
        private void chuanHoa(List<List<int>> parent_set, List<List<double>> d_set, List<List<double>> w_set, List<List<int>> l_set)
        {
            for (int ix = 0; ix < parent_set.Count; ix++)
            {
                List<int> parent = parent_set[ix];
                List<double> d = d_set[ix];
                List<double> w = w_set[ix];
                List<int> l = l_set[ix];
                bool isOk = false;
                while (!isOk)
                {
                    int m = parent.Count;
                    int n = parent.Count;
                    isOk = true;
                    List<List<int>> temp = new List<List<int>>();
                    for (int i = 0; i < n; i++)
                    {
                        temp.Add(new List<int>());
                    }
                    for (int i = 0; i < n; i++)
                    {
                        if (countOf(parent, parent[i]) > 2)
                        {
                            temp[parent[i]].Add(i);
                        }
                    }
                    for (int i = 0; i < n; i++)
                    {
                        if (temp[i].Count > 0)
                        {
                            isOk = false;
                            parent.Add(i);
                            d.Add(0);
                            w.Add(0);
                            l.Add(0);
                            parent[temp[i][temp[i].Count - 1]] = m;
                            parent[temp[i][temp[i].Count - 2]] = m;
                            m++;
                        }
                    }
                }
            }
        }
        private int findMinPos(List<double> a)
        {
            if (a.Count == 0)
            {
                return -1;
            }
            int pos = 0;
            double temp = Double.MaxValue;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] < temp)
                {
                    temp = a[i];
                    pos = i;
                }
            }
            return pos;
        }
        public void TimViTriThietBi(int sMC, int sDTD, int sDB)
        {
            List<SearchPosition> set = this.search;
            foreach(SearchPosition s in set)
            {
                s.reset();
            }
            int dem = sDB;
            int soMC = sMC;
            int soDTD = sDTD;
            int soDB = dem;
            double F_tong = Double.MaxValue;
            while (soMC > 0)
            {
                List<double> temp = new List<double>();
                List<int> viTriDatTrongCay = new List<int>();
                List<int> viTriCay = new List<int>();
                for (int j = 0; j < set.Count; j++)
                {
                    SearchPosition a = set[j];
                    double tong = 0;
                    for (int k = 0; k < set.Count; k++)
                    {
                        if (k != j)
                        {
                            tong += set[k].sumF1();
                        }
                    }
                    for (int i = 0; i < a.numNode; i++)
                    {
                        if (!a.getMC(i))
                        {
                            if (a.l[i] != 0 || a.d[i] != 0 || a.w[i] != 0)
                            {
                                a.setMC(i);
                                temp.Add(a.sumF1() + tong);
                                viTriCay.Add(j);
                                viTriDatTrongCay.Add(i);
                                a.refresh();
                            }
                        }
                    }
                }
                int vitri = findMinPos(temp);
                if (vitri == -1)
                {
                    break;
                }
                if (temp[vitri] > F_tong)
                {
                    break;
                }
                else
                {
                    F_tong = temp[vitri];
                }
                set[viTriCay[vitri]].setLastMC(viTriDatTrongCay[vitri]);
                set[viTriCay[vitri]].refresh();
                soMC -= 1;

            }
            while (soDTD > 0)
            {
                List<double> temp = new List<double>();
                List<int> viTriDatTrongCay = new List<int>();
                List<int> viTriCay = new List<int>();
                for (int j = 0; j < set.Count; j++)
                {
                    SearchPosition a = set[j];
                    double tong = 0;
                    for (int k = 0; k < set.Count; k++)
                    {
                        if (k != j)
                        {
                            tong += set[k].sumF1();
                        }
                    }
                    for (int i = 0; i < a.numNode; i++)
                    {
                        if (!a.getMC(i) && !a.getDTD(i) && a.currentM(i) < 4)
                        {
                            if (a.l[i] != 0 || a.d[i] != 0 || a.w[i] != 0)
                            {
                                a.setDTD(i);
                                temp.Add(a.sumF1() + tong);
                                viTriCay.Add(j);
                                viTriDatTrongCay.Add(i);
                                a.refresh();
                            }
                        }
                    }
                }
                int vitri = findMinPos(temp);
                if (vitri == -1)
                {
                    break;
                }
                if (temp[vitri] > F_tong)
                {
                    break;
                }
                else
                {
                    F_tong = temp[vitri];
                }
                set[viTriCay[vitri]].setLastDTD(viTriDatTrongCay[vitri]);
                set[viTriCay[vitri]].refresh();
                soDTD -= 1;

            }
            while (soDB > 0)
            {
                List<double> temp = new List<double>();
                List<int> viTriDatTrongCay = new List<int>();
                List<int> viTriCay = new List<int>();
                for (int j = 0; j < set.Count; j++)
                {
                    SearchPosition a = set[j];
                    double tong = 0;
                    for (int k = 0; k < set.Count; k++)
                    {
                        if (k != j)
                        {
                            tong += set[k].sumF2();
                        }
                    }
                    for (int i = 0; i < a.numNode; i++)
                    {
                        if (a.l[i] != 0 || a.d[i] != 0 || a.w[i] != 0)
                        {
                            if (!a.getMC(i) && !a.getDTD(i) && !a.getDB(i))
                            {

                                a.setDB(i);
                                temp.Add(a.sumF2() + tong);
                                viTriCay.Add(j);
                                viTriDatTrongCay.Add(i);
                                a.refresh();
                            }
                        }
                    }
                }
                int vitri = findMinPos(temp);
                if (vitri == -1)
                {
                    break;
                }
                if (temp[vitri] > F_tong)
                {
                    break;
                }
                else
                {
                    F_tong = temp[vitri];
                }
                set[viTriCay[vitri]].setLastDB(viTriDatTrongCay[vitri]);
                set[viTriCay[vitri]].refresh();
                soDB -= 1;
            }
            this.current_F = F_tong;
            tapMC.Clear();
            tapMC_toaDo.Clear();
            tapDTD.Clear();
            tapDTD_toaDo.Clear();
            tapDB.Clear();
            tapDB_toaDo.Clear();
            for (int i = 0; i < parent.Count; i++)
            {
                for (int j = 1; j < parent[i].Count; j++)
                {
                    if (search[i].mc[j])
                    {
                        tapMC.Add(c.cay_maCot[i][j][1]);
                        tapMC_toaDo.Add(c.cay_toaDo[i][j][1]);
                    }
                    else if (search[i].dtd[j])
                    {
                        tapDTD.Add(c.cay_maCot[i][j][1]);
                        tapDTD_toaDo.Add(c.cay_toaDo[i][j][1]);
                    }
                    else if (search[i].db[j])
                    {
                        tapDB.Add(c.cay_maCot[i][j][1]);
                        tapDB_toaDo.Add(c.cay_toaDo[i][j][1]);
                    }
                }
            }
        }
        public void TimSoLuongThietBi(double chiPhiDauTu)
        {
            List<SearchPosition> set = this.search;
            foreach (SearchPosition s in set)
            {
                s.reset();
            }
            int soMC = 0;
            int soDTD = 0;
            int soDB = 0;
            double F_tong = Double.MaxValue;
            int MaxMC = Convert.ToInt32(chiPhiDauTu / Setting.Pr);
            double F_best = 0.0;
            // set F_best = tong chi phi hien tai.
            foreach(SearchPosition s in set)
            {
                F_best += s.sumF2();
            }
            for(int nMC = 0; nMC<MaxMC; nMC++)
            {
                List<double> temp = new List<double>();
                List<int> viTriDatTrongCay = new List<int>();
                List<int> viTriCay = new List<int>();
                for (int j = 0; j < set.Count; j++)
                {
                    SearchPosition a = set[j];
                    double tong = 0;
                    for (int k = 0; k < set.Count; k++)
                    {
                        if (k != j)
                        {
                            tong += set[k].sumF1();
                        }
                    }
                    for (int i = 0; i < a.numNode; i++)
                    {
                        if (!a.getMC(i))
                        {
                            if (a.l[i] != 0 || a.d[i] != 0 || a.w[i] != 0)
                            {
                                a.setMC(i);
                                temp.Add(a.sumF1() + tong);
                                viTriCay.Add(j);
                                viTriDatTrongCay.Add(i);
                                a.refresh();
                            }
                        }
                    }
                }
                int vitri = findMinPos(temp);
                if (vitri == -1)
                {
                    break;
                }
                if (temp[vitri] > F_tong)
                {
                    break;
                }
                else
                {
                    F_tong = temp[vitri];
                }
                set[viTriCay[vitri]].setLastMC(viTriDatTrongCay[vitri]);
                set[viTriCay[vitri]].refresh();
            }
            while (soDTD > 0)
            {
                List<double> temp = new List<double>();
                List<int> viTriDatTrongCay = new List<int>();
                List<int> viTriCay = new List<int>();
                for (int j = 0; j < set.Count; j++)
                {
                    SearchPosition a = set[j];
                    double tong = 0;
                    for (int k = 0; k < set.Count; k++)
                    {
                        if (k != j)
                        {
                            tong += set[k].sumF1();
                        }
                    }
                    for (int i = 0; i < a.numNode; i++)
                    {
                        if (!a.getMC(i) && !a.getDTD(i) && a.currentM(i) < 4)
                        {
                            if (a.l[i] != 0 || a.d[i] != 0 || a.w[i] != 0)
                            {
                                a.setDTD(i);
                                temp.Add(a.sumF1() + tong);
                                viTriCay.Add(j);
                                viTriDatTrongCay.Add(i);
                                a.refresh();
                            }
                        }
                    }
                }
                int vitri = findMinPos(temp);
                if (vitri == -1)
                {
                    break;
                }
                if (temp[vitri] > F_tong)
                {
                    break;
                }
                else
                {
                    F_tong = temp[vitri];
                }
                set[viTriCay[vitri]].setLastDTD(viTriDatTrongCay[vitri]);
                set[viTriCay[vitri]].refresh();
                soDTD -= 1;

            }
            while (soDB > 0)
            {
                List<double> temp = new List<double>();
                List<int> viTriDatTrongCay = new List<int>();
                List<int> viTriCay = new List<int>();
                for (int j = 0; j < set.Count; j++)
                {
                    SearchPosition a = set[j];
                    double tong = 0;
                    for (int k = 0; k < set.Count; k++)
                    {
                        if (k != j)
                        {
                            tong += set[k].sumF2();
                        }
                    }
                    for (int i = 0; i < a.numNode; i++)
                    {
                        if (a.l[i] != 0 || a.d[i] != 0 || a.w[i] != 0)
                        {
                            if (!a.getMC(i) && !a.getDTD(i) && !a.getDB(i))
                            {

                                a.setDB(i);
                                temp.Add(a.sumF2() + tong);
                                viTriCay.Add(j);
                                viTriDatTrongCay.Add(i);
                                a.refresh();
                            }
                        }
                    }
                }
                int vitri = findMinPos(temp);
                if (vitri == -1)
                {
                    break;
                }
                if (temp[vitri] > F_tong)
                {
                    break;
                }
                else
                {
                    F_tong = temp[vitri];
                }
                set[viTriCay[vitri]].setLastDB(viTriDatTrongCay[vitri]);
                set[viTriCay[vitri]].refresh();
                soDB -= 1;
            }
            this.current_F = F_tong;
            tapMC.Clear();
            tapMC_toaDo.Clear();
            tapDTD.Clear();
            tapDTD_toaDo.Clear();
            tapDB.Clear();
            tapDB_toaDo.Clear();
            for (int i = 0; i < parent.Count; i++)
            {
                for (int j = 1; j < parent[i].Count; j++)
                {
                    if (search[i].mc[j])
                    {
                        tapMC.Add(c.cay_maCot[i][j][1]);
                        tapMC_toaDo.Add(c.cay_toaDo[i][j][1]);
                    }
                    else if (search[i].dtd[j])
                    {
                        tapDTD.Add(c.cay_maCot[i][j][1]);
                        tapDTD_toaDo.Add(c.cay_toaDo[i][j][1]);
                    }
                    else if (search[i].db[j])
                    {
                        tapDB.Add(c.cay_maCot[i][j][1]);
                        tapDB_toaDo.Add(c.cay_toaDo[i][j][1]);
                    }
                }
            }
        }
        public double getF(List<string> viTriMC, List<string> viTriDTD, List<string> viTriDB)
        {
            for(int i = 0; i< search.Count; i++)
            {
                search[i].reset();
            }
            for (int i = 0; i < parent.Count; i++)
            {
                for(int j =1; j<parent[i].Count; j++)
                {
                    if (j < c.cay_maCot[i].Count)
                    {
                        foreach (string vt in viTriMC)
                        {
                            if (c.cay_maCot[i][j].IndexOf(vt) >= 1)
                            {
                                search[i].setLastMC(j);
                                search[i].refresh();
                                break;
                            }
                        }
                        foreach (string vt in viTriDTD)
                        {
                            if (c.cay_maCot[i][j].IndexOf(vt) >= 1)
                            {
                                search[i].setLastDTD(j);
                                search[i].refresh();
                                break;
                            }
                        }
                        foreach (string vt in viTriDB)
                        {
                            if (c.cay_maCot[i][j].IndexOf(vt) >= 1)
                            {
                                search[i].setLastDB(j);
                                search[i].refresh();
                                break;
                            }
                        }
                    }
                }
            }
            double result = 0.0;
            foreach(SearchPosition s in search)
            {
                result += s.sumF2();
            }
            return result;
        }
        public double F_tong()
        {
            return this.current_F;
        }
        public List<string> layViTriDat(TYPE_OBJECT type)
        {
            if (type == TYPE_OBJECT.MAY_CAT)
            {
                return this.tapMC;
            }
            else if (type == TYPE_OBJECT.DAO_TU_DONG)
            {
                return this.tapDTD;
            }
            else
            {
                return this.tapDB;
            }
        }
        public List<double[]> layToaDoDat(TYPE_OBJECT type)
        {
            if (type == TYPE_OBJECT.MAY_CAT)
            {
                return this.tapMC_toaDo;
            }
            else if (type == TYPE_OBJECT.DAO_TU_DONG)
            {
                return this.tapDTD_toaDo;
            }
            else
            {
                return this.tapDB_toaDo;
            }
        }
    }
}
