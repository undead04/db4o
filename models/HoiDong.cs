using PROJECTDB.models;

namespace PROJECTDB.models
{
    public class HoiDong {
        public string MaHD = "";
        public string MaNamHoc = "";
        public int MaKhoa;
        public DateTime NgayBaoVe;
        public string DiaChiBaoVe = "";
        public GiaoVien? ChuTich;
        public GiaoVien? ThuKy;
        public GiaoVien? PhanBien;
        public List<DeTai> DeTais = new List<DeTai>();
    }

}
