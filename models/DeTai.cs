using PROJECTDB.models;

namespace PROJECTDB.models
{
   public class DeTai {
        public string MaDT = "";
        public int MaKhoa;
        public string TenDT = "";
        public string MaNamHoc = "";
        public GiaoVien? GVHuongDan ;
        public DateTime ThoiGianBatDau;
        public DateTime ThoiGianKetThuc;
        public HoiDong? HoiDong;
        public List<DeTaiSinhVien> SinhViens = new List<DeTaiSinhVien>();
        public List<TaiLieu> TaiLieus = new List<TaiLieu>();
    }
}