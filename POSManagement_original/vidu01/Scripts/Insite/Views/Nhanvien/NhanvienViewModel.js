var AjaxObj = function () {
    this.SuccessHandleFunction = null;
    this.Action = function (parameters, ajaxUrl, contentType, method) {
        $.ajax({
            type: method,
            url: ajaxUrl,
            async: false,
            contentType: contentType,
            data: parameters,
            success: function (e) {
                if (AjaxInstance.SuccessHandleFunction != null) {
                    AjaxInstance.SuccessHandleFunction(e.Data);
                }
            },
            error: function (e) {
                alert('Lỗi không thể kết nối đến server.');
            }
        });

    };
};
var AjaxInstance = new AjaxObj();

var NhanvienViewModel = function () {
    var self = this;
    // Data
    self.List = ko.observableArray([]);
    self.Current = ko.observable(null);
    self.SearchName = ko.observable("");
    self.ListUser = ko.observableArray([]);


    //function
    self.CountSearch = ko.computed(function () {
        var i = 0;
        ko.utils.arrayForEach(self.List(), function (item) {
            if (item.IsVisible() == true)
                i++;
        });
        return i;
    });


    self.InitData = function () {
        self.InitList();
    };
    self.InitList = function () {
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data != null && data.length > 0) {

                var index = 0;
                var listItems = ko.utils.arrayMap(data, function (item) {
                    index++;
                    return new Nhanvien(item.nhanvien.NhanvienId, item.nhanvien.Hoten, item.nhanvien.Diachi, item.nhanvien.Email, item.nhanvien.Sdt
                        , item.user.UserId, item.user.Password);
                });
                self.List.push.apply(self.List, listItems);
                if (self.List().length > 0)
                    self.Current(self.List()[0]);
            }
        };

        AjaxInstance.Action("", "http://localhost:56173/Nhanvien/GetNhanvienItems", 'application/json; charset=utf-8', 'get');
    };

    //Action
    self.btnSetCurrent = function (item) {
        self.Thoat();
        item.IsEdit(false);
        item.OldValue(ko.toJS(item));
        self.Current(item);

    };
    self.btnSearchName = function () {
        var iCurr = null;
        ko.utils.arrayForEach(self.List(), function (item) {
            if (item.Hoten().toLowerCase().indexOf(self.SearchName().toLowerCase()) != -1) {
                if (iCurr == null) {
                    iCurr = item;
                    self.Current(item);
                }
                item.IsVisible(true);
            }
            else {
                item.IsVisible(false);
            }
        });
    };
    self.CreateHanghoa = function () {
        self.Thoat();
        var newItem = new Nhanvien(null, null, null, null, null, null);
        newItem.IsEdit(true);
        newItem.IsCreate(true);
        self.Current(newItem);

    };

    self.Thoat = function (item) {
        if (!item)
            item = self.Current();

        if (!item.OldValue())
            return;

        var obj = JSON.parse(ko.toJSON(item.OldValue())); // Lấy oldValue
        if (obj.NhanvienId) {// Nếu không có Id Nghĩa là người dùng ấn Edit rồi hũy   
            item.NhanvienId(obj.NhanvienId);
            item.Hoten(obj.Hoten);
            item.Diachi(obj.Diachi);
            item.Email(obj.Email);
            item.Sdt(obj.Sdt)
            item.UserId(obj.UserId);
            item.Password(obj.Password);
        }
        item.IsEdit(false);
        self.Current(self.List()[0]);
    };

    self.Edit = function (item) {
        self.Thoat();
        item.IsEdit(true);
        item.OldValue(ko.toJS(item));
        self.Current(item);
    }

    self.luuHanghoa = function (item) {
        if (!self.Current().isValid()) // Kiểm tra xem model này có đúng hay không
            return // Nếu sai thì return.

        var codeTrung = false;
        ko.utils.arrayForEach(self.List(), function (val) {
            if (val.UserId() == item.UserId() && val != item) {
                alert("Tên tài khoản \"" + val.UserId() + "\" đã tồn tại cho nhân viên \"" + val.Hoten() + "\".");
                codeTrung = true;
                return;
            }
        });
        if (codeTrung)
            return;

        var jsonData = ko.toJSON(self.Current());
        if (self.Current().IsCreate() == false) {
            AjaxInstance.SuccessHandleFunction = function (data) {
                if (data == "success") {
                    self.Current().OldValue(null);
                    alert("Sửa thành công");
                } else {
                    self.Thoat();
                    alert(data);
                }
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/Nhanvien/EditNhanvienItem", 'application/json; charset=utf-8', 'post');
        }
        else {
            AjaxInstance.SuccessHandleFunction = function (data) {
                var arr = data.split('-+-');
                if (arr[0] == "success") {
                    item.NhanvienId(arr[1]);
                    self.List.push(item);
                    alert("Thêm thành công")
                } else {
                    alert(arr[0]);
                }                
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/Nhanvien/addNhanvienItem", 'application/json; charset=utf-8', 'post');

        }
        self.Current().IsCreate(false);
        self.Current().IsEdit(false);
    };
    self.remove = function (item) {
        if (confirm("Bạn có chắc chắn muốn xóa: " + item.Hoten()) == false) {
            return;
        }
        var jsonData = ko.toJSON(item);
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data == "success") {
                self.List.remove(item);
                alert("Xóa thành công");
            } else {
                alert(data);
            }
        }
        AjaxInstance.Action(jsonData, "http://localhost:56173/Nhanvien/deleteNhanvienItem", 'application/json; charset=utf-8', 'post');

        self.Current(self.List()[0]);
    }


}
$(document).ready(function () {
    var model = new NhanvienViewModel();
    model.InitData();

    ko.validation.init({
        decorateElement: true,
        errorMessageClass: 'text-error',
        insertMessages: true,
    });
    ko.applyBindings(model);
});

