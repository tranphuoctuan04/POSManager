var Hanghoa = function (HanghoaId, Code, Ten, Giaban, NgayGiaban, NhomHanghoaId, Dangban, TenNhomHanghoa) {
    var self = this;
    self.HanghoaId = ko.observable(HanghoaId).extend({});
    self.Code = ko.observable(Code).extend({ min: { params: 0, message: '*Code của hàng hóa không được âm.' }, max: { params: 9.22337203685478E+18, message: '*Code của hàng hóa không được âm.' }, });
    self.Ten = ko.observable(Ten).extend({ required: { params: true, message: '*Tên hàng hóa không được để trống' }, });
    self.Giaban = ko.observable(Giaban).extend({});
    self.NgayGiaban = ko.observable(NgayGiaban).extend({});
    self.NhomHanghoaId = ko.observable(NhomHanghoaId).extend({});
    self.Dangban = ko.observable(Dangban).extend({});

    self.TenNhomHanghoa = ko.observable(TenNhomHanghoa).extend({});

    // Addon
    self.IsCreate = ko.observable(false);
    self.IsEdit = ko.observable(false);
    self.IsVisible = ko.observable(true);
    self.OldValue = ko.observable({});

    // Validate.
    this.Errors = ko.validation.group(this);
    this.isValid = ko.computed(function () {
        return self.Errors().length == 0;
    });

    //
    self.getDate = ko.computed(function () {
        //alert("in");
        var ngayGiaBan = self.NgayGiaban();
        // alert(ngayGiaBan);
        try {
            var date = (eval(ngayGiaBan.replace(/\/Date\((\d+)\)\//g, "new Date($1)")));
            return date.toLocaleDateString();
        } catch (ex) {
            try {
                var val = ngayGiaBan + "";
                var date = new Date(val);
                return date.toLocaleDateString();
            } catch (ex) {
                return ex.message;
            }
        }
    }, this);
}
