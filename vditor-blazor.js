// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

vditorScript = () => {
  new Vditor('vditor', {
    height: 360,
    cache: {
      enable: false,
    },
    value: 'Markdown 配置',
  })
}