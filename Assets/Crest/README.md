
<img src="https://raw.githubusercontent.com/huwb/crest-oceanrender/master/logo/crest-oceanrender-logotype1.png" width="214">

&nbsp;


# Intro

*Crest* is a technically advanced ocean renderer implemented in Unity3D 2018.3 and later. The version hosted here targets the **built-in render pipeline**, a link to the LWRP version on the Asset Store is below.

![Teaser](https://raw.githubusercontent.com/huwb/crest-oceanrender/master/img/teaser5.png)

**Discord for news/updates/discussions:** https://discord.gg/g7GpjDC

**Twitter:** [@crest_ocean](https://twitter.com/@crest_ocean)

**URP asset:** [Crest Ocean System URP](https://assetstore.unity.com/detail/tools/particles-effects/crest-ocean-system-lwrp-urp-141674)

**HDRP asset:** Coming soon.

# Showcase Gallery

*Your game here! We're looking for projects to showcase - if you upload a video of your work to youtube and send us a link (or create a pull request) we'll put a thumbnail here and link to it.*

<a href="https://www.youtube.com/watch?feature=player_embedded&v=nsQJ5IJVHVw" target="_blank"><img src="https://img.youtube.com/vi/nsQJ5IJVHVw/0.jpg" alt="Hope Adrift Gameplay & Release Trailer" width="240" height="180" /></a>
<a href="https://www.youtube.com/watch?feature=player_embedded&v=Qfy5P4Zygvs" target="_blank"><img src="https://img.youtube.com/vi/Qfy5P4Zygvs/0.jpg" alt="Morild Navigator" width="240" height="180" /></a>
<a href="https://www.youtube.com/watch?feature=player_embedded&v=LNIQ6RF5lrw" target="_blank"><img src="https://img.youtube.com/vi/LNIQ6RF5lrw/0.jpg" alt="Blue Water Dev Diary - CIWS Expo" width="240" height="180" /></a>
<a href="https://www.youtube.com/watch?feature=player_embedded&v=3i6VpdKw2Q0" target="_blank"><img src="https://img.youtube.com/vi/3i6VpdKw2Q0/0.jpg" alt="Crest Ocean System - Pirate Cove Example Scene" width="240" height="180" /></a>
<a href="https://www.youtube.com/watch?feature=player_embedded&v=m2ZojyD4PZc" target="_blank"><img src="https://img.youtube.com/vi/m2ZojyD4PZc/0.jpg" alt="Critter Cove & Crest Trailer" width="240" height="180" /></a>
<a href="https://www.youtube.com/watch?feature=player_embedded&v=zCeK_Kdxqa0" target="_blank"><img src="https://img.youtube.com/vi/zCeK_Kdxqa0/0.jpg" alt="Of Ships & Scoundrels - Crest Demo" width="240" height="180" /></a>
<a href="https://www.youtube.com/watch?feature=player_embedded&v=HVlJa2J0wSc" target="_blank"><img src="https://img.youtube.com/vi/HVlJa2J0wSc/0.jpg" alt="Rogue Waves" width="240" height="180" /></a>

# Documentation

Refer to [USERGUIDE.md](https://github.com/huwb/crest-oceanrender/blob/master/USERGUIDE.md) for full documentation, including **Initial setup steps**.

There is also a getting started video here: https://www.youtube.com/watch?v=qsgeG4sSLFw&t=142s .

# Prerequisites

* Unity version:
  * The SRP assets on the Asset Store specify the minimum version required.
  * Releases on this GitHub target the built-in render pipeline, and each release specifies which version of Unity it was developed on. Currently Unity 2018.3 or later is the minimum version.
* *Crest* example content:
  * The content requires a layer named *Terrain* which should be added to your project.
  * The post processing package is used (for aesthetic reasons), if this is not present in your project you will see an unassigned script warning which you can fix by removing the offending script.
* .NET 4.x runtime
* Direct X11 or Vulkan, other platforms targeting [shader compilation target](https://docs.unity3d.com/Manual/SL-ShaderCompileTargets.html) 4.5 or above may work but we cannot provide support for, or test on, other APIs ourselves.
* Due to the previous point, *Crest* unfortunately does not support WebGL

# Releases

The best way to obtain *Crest* is take the latest version in the master branch by forking/cloning this repository or by using the green download button above. We rely heavily on the community to help us test new features before creating releases. Once features are settled they are integrated into the SRP assets (linked above).

Releases are published irregularly and posted on the [Releases page](https://github.com/huwb/crest-oceanrender/releases). Unity packages are uploaded with each release. The lastest release version is 9.0.0.


# Issues

If you encounter an issue, please search the [Issues page](https://github.com/huwb/crest-oceanrender/issues) to see if there is already a resolution, and if you don't find one then please report it as a new issue.

There are a few issues worth calling out here:

* *Crest* does not yet support *HDRP*. If you would find such support useful, please feel free to comment in issue #201.
* Sky solutions such as Azure[Sky] requires some code to be added to the ocean shader for the fogging/scattering to work. This is a requirement of these products which typically come with instructions for what needs to be added. See issue #62 for an example.
* Issue with LWRP and VR - refraction appears broken due to what seems to be a bug in LWRP. See issue #206.
* This built-in render pipeline version of crest requires the *Draw Instanced* option on terrains to be disabled at start time. It can be re-enabled subsequently after the depth cache is populated. See issue #158.
* *Crest* requires compute and does not support WebGL
