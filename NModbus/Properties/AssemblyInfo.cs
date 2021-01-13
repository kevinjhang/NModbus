using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(false)]
[assembly: ComVisible(false)]

#if !SIGNED
[assembly: InternalsVisibleTo("NModbus.UnitTests")]
[assembly: InternalsVisibleTo("NModbus.IntegrationTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
#endif
