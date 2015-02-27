var Nhanvien = function (NhanvienId, Hoten, Diachi, Email, Sdt, UserId, Password) {
    var self = this;
    self.NhanvienId = ko.observable(NhanvienId).extend({});
    self.Hoten = ko.observable(Hoten).extend({ required: { params: true, message: '*Tên không được để trống.' }, });
    self.Diachi = ko.observable(Diachi).extend({ required: { params: true, message: '*Địa chỉ không được để trống.' }, });
    self.Email = ko.observable(Email).extend({ required: { params: true, message: '*Email không được để trống.' }, pattern: { params: /^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/, message: '*Phải nhập chính xác email' }, });
    self.Sdt = ko.observable(Sdt).extend({ required: { params: true, message: '*Số điện thoại không được để trống hoặc có chữ.' }, });
    self.UserId = ko.observable(UserId).extend({ minLength: { params: 6, message: '*Tài khoản phải lớn hơn 6 ký tự' }, maxLength: { params: 32, message: '*Tài khoản phải nhỏ hơn 32 ký tự' }, required: { params: true, message: '*Bạn phải nhập vào tên tài khoản' }, });
    self.Password = ko.observable(Password).extend({ maxLength: { params: 32, message: '*Mật khẩu phải nhỏ hơn 32 ký tự' }, required: { params: true, message: '*Bạn phải nhập vào mật khẩu' }, minLength: { params: 6, message: '*Mật khẩu phải lớn hơn 6 ký tự' }, });

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
}
