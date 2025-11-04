using PROJECTDB.models;
namespace PROJECTDB.models
{
    public class NamHoc {
        public string MaNamHoc = "";
        public DateTime NgayBatDau;
        public DateTime NgayKetThuc;
        public List<DeTai> DeTais = new List<DeTai>();
        public List<HoiDong> HoiDongs = new List<HoiDong>();

    }
    
}

