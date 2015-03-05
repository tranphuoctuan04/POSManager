var AjaxObj = function () {
    this.SuccessHandleFunction = null;
    this.Action = function (parameters, ajaxUrl, contentType, method) {
        $.ajax({
            type: method,
            url: ajaxUrl,
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

var UserViewModel = function () {
    var self = this;
    // Data
    self.List = ko.observableArray([]);
    self.Current = ko.observable(null);
    self.SearchName = ko.observable("");

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
            //alert(data);
            if (data != null && data.length > 0) {
                var index = 0;
                var listItems = ko.utils.arrayMap(data, function (item) {
                    //alert(item.co_name);
                    index++;
                    return new User(item.UserId, item.NhanvienId, item.Password, item.Nhanvien);
                });
                // alert(listItems);
                self.List.push.apply(self.List, listItems);
                if (self.List().length > 0)
                    self.Current(self.List()[0]);
            }
        };

        AjaxInstance.Action("", "http://localhost:56173/User/GetUserItems", 'application/json; charset=utf-8', 'get');
    };

    //Action
    self.btnSetCurrent = function (item) {
        item.IsEdit(false);
        item.OldValue(ko.toJS(item));
        self.Current(item);
    };
    self.btnSearchName = function () {
        var iCurr = null;
        ko.utils.arrayForEach(self.List(), function (item) {
            if (item.NhanvienId().toLowerCase().indexOf(self.SearchName().toLowerCase()) != -1) {
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
        var newItem = new User(null, null, null, null);
        newItem.IsEdit(true);
        newItem.IsCreate(true);
        self.Current(newItem);
    };

    self.Thoat = function (item) {
        var obj = JSON.parse(ko.toJSON(item.OldValue())); // Lấy oldValue
        if (obj.UserId) {// Nếu không có Id Nghĩa là người dùng ấn Edit rồi hũy   
            item.UserId(obj.UserId); 
            item.NhanvienId(obj.NhanvienId); 
            item.Password(obj.Password); 
            item.Nhanvien(obj.Nhanvien) 
        }
        item.IsEdit(false);
        self.Current(self.List()[0]);
    };
    
    self.Edit = function(item){
        item.IsEdit(true);
        item.OldValue(ko.toJS(item));
        self.Current(item);
    }

    self.luuHanghoa = function (item) {
        if (!self.Current().isValid()) // Kiểm tra xem model này có đúng hay không
            return // Nếu sai thì return.

        var jsonData = ko.toJSON(self.Current());
        if (self.Current().IsCreate() == false)
        {
            AjaxInstance.SuccessHandleFunction = function (data) {
                alert(data);
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/User/EditUserItem", 'application/json; charset=utf-8', 'post');
        }
        else
        {
            AjaxInstance.SuccessHandleFunction = function (data) {
                var arr = data.split('-+-');
                if (arr[0] == "Thêm thành công") {
                    item.UserId = arr[1];
                    self.List.push(item);
                }
                alert(arr[0]);
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/User/addUserItem", 'application/json; charset=utf-8', 'post');
            
        }
        self.Current().IsCreate(false);
        self.Current().IsEdit(false);
    };
    self.remove = function(item)
    {
        if (confirm("Bạn có chắc chắn muốn xóa: " + item.NhanvienId()) == false) {
            return;
        }
        var jsonData = ko.toJSON(item);
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data == "Xóa thành công") self.List.remove(item);
            alert(data);        
        }
        AjaxInstance.Action(jsonData, "http://localhost:56173/User/deleteUserItem", 'application/json; charset=utf-8', 'post');
        
        self.Current(self.List()[0]);
    }
};

$(document).ready(function () {
    var model = new UserViewModel();
    model.InitData();
    
    ko.validation.init({
        decorateElement: true,
        errorMessageClass: 'text-error',
        insertMessages: true,
    });
    ko.applyBindings(model);
});

