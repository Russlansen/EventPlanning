$(function () {
    var count = 1;

    function remove() {
        var row = this.closest("tr")
        count--;
        row.remove();
        var trTheme = $("tr[data-group='theme']");
        for(i = 0; i < count; i++)
        {
            trTheme[i].setAttribute("data-count", i);
            var cellTheme = trTheme[i].children[0];
            var cellDesc = trTheme[i].children[1];
            var childrenCount = cellTheme.childElementCount;
            for (var j = 0; j < childrenCount; j++) {
                if (j == 0) {
                    cellTheme.children[j].setAttribute("name", "Theme[" + i + "].Key");
                    cellDesc.children[j].setAttribute("name", "Theme[" + i + "].Value");
                } else {
                    cellTheme.children[j].setAttribute("name", "Theme[" + i + "].SubTheme.Key");
                    cellDesc.children[j].setAttribute("name", "Theme[" + i + "].SubTheme.Value");
                }  
            }
        }         
    }

    function removeSubTheme() {
        var tr = $(this).closest("tr");
        var cellTheme = tr.children("td")[0];
        var cellDesc = tr.children("td")[1];
        var cellRemove = tr.children("td")[2];
        var removeBtn = cellRemove.lastChild;
        removeBtn.remove();
        var addSubBtn = document.createElement('input');
        addSubBtn.setAttribute("type", "button");
        addSubBtn.setAttribute("class", "btn btn-info");
        addSubBtn.setAttribute("value", "Добавить подтему");
        addSubBtn.addEventListener("click", addSubTheme);
        cellTheme.lastChild.remove();
        cellDesc.lastChild.remove();
        cellRemove.appendChild(addSubBtn);
    }

    $("#addSubTheme").click(addSubTheme);

    function addSubTheme() {
        var row = this.closest("tr")
        var localCount = $(this).closest('tr').data('count');
        row.children[0].innerHTML += "<textarea type=\"text\"  name=\"Theme["+localCount+"].SubTheme.Key\" class=\"sub-field form-control\" placeholder=\"Введите подтему\"></textarea>";
        row.children[1].innerHTML += "<textarea type=\"text\" name=\"Theme["+localCount+"].SubTheme.Value\" class=\"sub-field form-control\" placeholder=\"Введите описание\"></textarea>";
        var removeBtn = document.createElement('input');
        removeBtn.setAttribute("type", "button");
        removeBtn.setAttribute("class", "btn btn-danger");
        removeBtn.setAttribute("value", "Удалить подтему");
        removeBtn.addEventListener("click", removeSubTheme);
        row.children[2].lastChild.remove();
        row.children[2].appendChild(removeBtn)
    }

    $("#addTheme").click(function () {
        var tr = document.createElement('tr');
        tr.setAttribute("data-count", count);
        tr.setAttribute("data-group", "theme");
        var tdTitle = document.createElement('td');
        var tdDescription = document.createElement('td');
        var tdRemove = document.createElement('td');
        var tdAddSubTheme = document.createElement('td');
        tdRemove.setAttribute("class", "col-md-1");
        tdAddSubTheme.setAttribute("class", "col-md-1");
        var removeBtn = document.createElement('input');
        removeBtn.setAttribute("type", "button");
        removeBtn.setAttribute("class", "btn btn-danger");
        removeBtn.setAttribute("value", "Удалить тему");
        removeBtn.addEventListener("click", remove);

        var addSubBtn = document.createElement('input');
        addSubBtn.setAttribute("type", "button");
        addSubBtn.setAttribute("class", "btn btn-info");
        addSubBtn.setAttribute("value", "Добавить подтему");
        addSubBtn.addEventListener("click", addSubTheme);

        tdTitle.innerHTML = "<textarea type=\"text\" name=\"Theme["+count+"].Key\" class=\"form-control\" placeholder=\"Введите тему\"></textarea>";
        tdDescription.innerHTML = "<textarea type=\"text\" name=\"Theme["+count+"].Value\" class=\"form-control\" placeholder=\"Введите описание\"></textarea>";
        tdRemove.appendChild(removeBtn);
        tdAddSubTheme.appendChild(addSubBtn);

        tr.appendChild(tdTitle);
        tr.appendChild(tdDescription);
        tr.appendChild(tdAddSubTheme);
        tr.appendChild(tdRemove);
        $("table tr").eq(count++).after(tr);
    })  
})