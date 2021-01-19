class filter {
    constructor () {
        this.registrations = {};
    }
    
    register(elementName) {
        const self = this;
        self.registrations[elementName] = "none";
        
        $(elementName).on('change', function (e) {
            self.registrations[elementName] = $(this).val();
            
            self.onEvent(self);
        });
    }

    onEvent(self) {
        $(".filterable").hide()
            .filter(function () {
                if (filter === "none")
                    return true;

                var isValid = true;
                for (var key in self.registrations) {
                    var value = self.registrations[key];
                    
                    if (isValid === true)
                        if (value !== "none")
                            isValid = $(this).html().includes(value) || $(this).hasClass(value);
                }
                
                return isValid;
            }).closest(".filterable").show();
    }
}