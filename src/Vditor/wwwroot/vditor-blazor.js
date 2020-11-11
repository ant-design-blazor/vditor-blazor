// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API
window.vditorBlazor = window.vditorBlazor || {
  // create vditor instance
  createVditor: (domRef, editor, value, options = {}) => {
    options = convertJsonKey(options);

    domRef.Vditor = new Vditor(domRef, {
      ...options,
      theme: 'ant-design',
      icon: 'ant',
      cache: {
        enable: false,
      },
      value,
      after: () => {
        editor.invokeMethodAsync('HandleRendered', value);
      },
      input: (value) => {
        editor.invokeMethodAsync('HandleInput', value);
      },
      focus: (value) => {
        editor.invokeMethodAsync('HandleFocus', value);
      },
      blur: (value) => {
        editor.invokeMethodAsync('HandleBlur', value);
      },
      esc: (value) => {
        editor.invokeMethodAsync('HandleEscPress', value);
      },
      ctrlEnter: (value) => {
        editor.invokeMethodAsync('HandleCtrlEnterPress', value);
      },
      select: (value) => {
        editor.invokeMethodAsync('HandleSelect', value);
      },
    });
  },
  getValue: (domRef) => {
    return domRef.Vditor.getValue();
  },
  getHTML: (domRef) => {
    return domRef.Vditor.getHTML();
  },
  setValue: (domRef, value, clearStack = false) => {
    domRef.Vditor.setValue(value, clearStack);
  },
  insertValue: (domRef, value, render = true) => {
    domRef.Vditor.insertValue(value, render);
  },
  destroy: (domRef) => {
    domRef.Vditor.destroy();
  },
  preview: (domRef, editor, markdown, options = {}) => {
    options = convertJsonKey(options);
    Vditor.preview(domRef, markdown, {
      ...options,
      after() {
        if (options.handleAfter) {
          editor.invokeMethodAsync('HandleAfter');
        }
      }
    });
  }
}

var editorObj = null;
var setEditor = function (editor) {
    editorObj = editor;
}

var ClickCustomToolbar = function (name) {
    editorObj.invokeMethodAsync("HandleClickCustomToolbar", name)
}

var convertJsonKey = function (jsonObj) {
  var result = {};
  for (key in jsonObj) {
    var keyval = jsonObj[key];
    if (keyval == null) {
      continue;
    }
      key = key.replace(key[0], key[0].toLowerCase());

      if (typeof (keyval) == "object") {
          var data = JSON.stringify(keyval);
          var json = JSON.parse(data, function (k, v) {
              if (v.indexOf && v.indexOf('function') > -1) {
                  return eval("(function(){return " + v + "})()")
              }
              return v;
          });
          keyval = json
          console.log(typeof (keyval), data)
      }

    result[key] = keyval;
  }
  return result;
}