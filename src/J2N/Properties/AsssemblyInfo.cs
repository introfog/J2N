﻿using J2N;
using System;
using System.Runtime.CompilerServices;

[assembly: CLSCompliant(true)]

[assembly: InternalsVisibleTo("J2N.Benchmarks, PublicKey=" + AssemblyKeys.PublicKey)]
[assembly: InternalsVisibleTo("J2N.Tests, PublicKey=" + AssemblyKeys.PublicKey)]
[assembly: InternalsVisibleTo("J2N.Tests.xUnit, PublicKey=" + AssemblyKeys.PublicKey)]