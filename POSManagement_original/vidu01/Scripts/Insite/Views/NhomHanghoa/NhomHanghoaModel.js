var NhomHanghoa = function (NhomHanghoaId, Code, Ten) {
    var self = this;
    self.NhomHanghoaId = ko.observable(NhomHanghoaId).extend({});
    self.Code = ko.observable(Code).extend({ min: { params: 0, message: 'ID của nhóm hàng hóa không được âm.' }, max: { params: 9.22337203685478E+18, message: 'ID của nhóm hàng hóa phải lớn hơn 0' }, });
    self.Ten = ko.observable(Ten).extend({ required: { params: true, message: 'Tên không được để trống' }, });
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
