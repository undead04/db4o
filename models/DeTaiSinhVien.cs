using PROJECTDB.models;

namespace PROJECTDB.models
{
    public class DeTaiSinhVien {
        public DeTai? DeTai;
        public SinhVien? SinhVien;
        public double DiemTrungBinh;
        public string KetQua = "";

        public List<Diem> Diems = new List<Diem>();
    }
}