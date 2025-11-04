using PROJECTDB.models;
namespace PROJECTDB.models
{
    public class Khoa
    {
        public int MaKhoa;
        public string TenKhoa = "";
        public List<SinhVien> SinhViens = new List<SinhVien>();
        public List<GiaoVien> GiaoViens = new List<GiaoVien>();
        public List<DeTai> DeTais = new List<DeTai>();
        public List<HoiDong> HoiDongs = new List<HoiDong>();
    }   
}

