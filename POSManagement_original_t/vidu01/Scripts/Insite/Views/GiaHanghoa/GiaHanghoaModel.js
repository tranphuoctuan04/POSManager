var GiaHanghoa = function (GiaHanghoaId, Code, HanghoaId, Gia, NgayApdung) {
    var self = this;
    self.GiaHanghoaId = ko.observable(GiaHanghoaId).extend({});
    self.Code = ko.observable(Code).extend({ required: { params: true, message: '*Bạn phải nhập vào code của giá hàng hóa' }, min: { params: 0, message: '*Code của giá hàng hóa không được âm' }, max: { params: 2147483647, message: '*Code của giá hàng hóa không được âm.' }, });
    self.HanghoaId = ko.observable(HanghoaId).extend({});
    self.Gia = ko.observable(Gia).extend({ required: { params: true, message: '*Bạn phải nhập giá của hàng hóa.' }, min: { params: 1, message: '*Giá của hàng hóa phải lớn hơn 0' }, max: { params: 2147483647, message: '*Giả của hàng hóa phải lớn hơn 0' }, });
    self.NgayApdung = ko.observable(NgayApdung).extend({ required: { params: true, message: '*Bạn phải nhập vào ngày bắt đầu áp dụng' }, });
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

    // Để convert epoch to date.
    self.getDate = ko.computed(function () {
        //alert("in");
        var ngayApdung = self.NgayApdung();
        // alert(ngayApdung);
        try {
            var date = (eval(ngayApdung.replace(/\/Date\((\d+)\)\//g, "new Date($1)")));
            return date.toLocaleDateString();
        } catch (ex) {
            try{
                var val = ngayApdung + "";
                var date = new Date(val);
                return date.toLocaleDateString();
            } catch (ex) {
                return ex.message;
            }
        }
    }, this);

    self.getDateForInput = function () {
        var ngayApdung = self.getDate();
        var arr = ngayApdung.split("/");
        return pad(arr[2]) + "-" + pad(arr[0]) + "-" + pad(arr[1]);
    };

    function pad(d) {
        return (d < 10) ? '0' + d.toString() : d.toString();
    }
}
