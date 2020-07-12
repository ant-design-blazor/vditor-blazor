// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

createVditor = (domRef) => {
  new Vditor(domRef, {
    height: 360,
    cache: {
      enable: false,
    },
    value: 'Markdown 配置',
  })
}