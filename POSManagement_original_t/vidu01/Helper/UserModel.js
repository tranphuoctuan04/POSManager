var User = function (UserId, NhanvienId, Password, Nhanvien){
var self=this;
self.UserId = ko.observable(UserId).extend({minLength: {params: 6,message: '*Tài khoản phải lớn hơn 6 ký tự'},maxLength: {params: 32,message: '*Tài khoản phải nhỏ hơn 32 ký tự'},required: {params: true,message: '*Bạn phải nhập vào tên tài khoản'},});
self.NhanvienId = ko.observable(NhanvienId).extend({});
self.Password = ko.observable(Password).extend({maxLength: {params: 32,message: '*Mật khẩu phải nhỏ hơn 32 ký tự'},required: {params: true,message: '*Bạn phải nhập vào mật khẩu'},minLength: {params: 6,message: '*Mật khẩu phải lớn hơn 6 ký tự'},});
self.Nhanvien = ko.observable(Nhanvien).extend({});
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
