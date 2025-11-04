namespace PROJECTDB.models
{
    public abstract class Nguoi {
        public string Ma;               // mã chung, có thể là MaSV hoặc MaGV
        public int MaKhoa;
        public string HoTen;
        public string DiaChi;
        public string SoDienThoai;
        public Nguoi(string ma, int maKhoa, string hoTen, string diaChi, string soDienThoai) {
            this.Ma = Guid.NewGuid().ToString();
            this.MaKhoa = maKhoa;
            this.HoTen = hoTen;
            this.DiaChi = diaChi;
            this.SoDienThoai = soDienThoai;
        }
    }

}