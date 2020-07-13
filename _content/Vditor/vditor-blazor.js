// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

createVditor = (domRef, editor, value) => {
  domRef.Vditor = new Vditor(domRef, {
    height: 360,
    cache: {
      enable: false,
    },
    value,
    input: (value) => {
      editor.invokeMethodAsync('OnInput', value);
    }
  });
}

getValue = (domRef) => {
  return domRef.Vditor.getValue();
}

setValue = (domRef, value) => {
  console.log('setValue', value);
  domRef.Vditor.setValue(value, true);
}