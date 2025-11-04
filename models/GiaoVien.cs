using PROJECTDB.models;

namespace PROJECTDB.models
{
    public class GiaoVien:Nguoi
    {
        public string HocVi;
        public string ChuyenNganh;
        public List<DeTai> DeTaisHuongDan = new List<DeTai>();
        public List<HoiDong> HoiDongsChuTich = new List<HoiDong>();
        public List<HoiDong> HoiDongsThuKy = new List<HoiDong>();
        public List<HoiDong> HoiDongsPhanBien = new List<HoiDong>();

        public GiaoVien(string maGV, int maKhoa, string tenGV, string diaChi,
                        string soDienThoai, string hocVi, string chuyenNganh):
                        base(maGV, maKhoa, tenGV, diaChi, soDienThoai) {
            this.HocVi = hocVi;
            this.ChuyenNganh = chuyenNganh;
        }
    }
}