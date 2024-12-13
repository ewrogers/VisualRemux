# VisualRemux

Cross-platform GUI toolkit providing easy wrappers around `ffmpeg` and `mkvtoolnix` CLI tools for various workflows
involving video files.

Written in .NET 9 using [Avalonia](https://docs.avaloniaui.net/) with [Semi Design](https://irihitech.github.io/Semi.Avalonia/) theme.

## Requirements

- .NET 9 Runtime - [Download](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- ffmpeg - [Download](https://ffmpeg.org/download.html)
- MKVToolNix - [Download](https://mkvtoolnix.download/)

## Features

### Remux

**Status:** 🔧 In Development

The `Remux` tool will make it easy to convert from one video container format to another.
For example, `.mkv` to `.mp4` files without re-encoding the video or audio streams.

Will support bulk operations and drag-and-drop of files for ease of use.

Uses `ffmpeg` to do the remux.

### Split & Stitch

**Status:** 💡 Planned

The `Split & Stitch` tool will make it easy to split and stitch a single video into multiple videos by
timestamp or chapter. This is especially useful for TV shows that have two or more episodes in a single video,
allowing them to be separated into singular episodes.

Will support bulk operations and drag-and-drop of files for ease of use.

Uses `mkvmerge` to do the splitting and re-combining of parts.

### Rename

**Status:** 💡 Planned

The `Rename` tool will make it easy to rename large sets of files into a standardized format, optionally removing
any special characters or punctuation. This will include show name, year, season/episode, and title format options.

Will support bulk operations and drag-and-drop of files for ease of use.

This tool will also be featured as a "pipeline" stage for other tools allowing them to be chained after remuxing,
splitting, or other tool outputs.

### Title Scan

**Status:** 💡 Planned

The `Title Scan` tool will make it easy to detect episode names from video files by scanning the intro and applying OCR
(optical character recognition). The intro interval and scanning parameters will be configurable.

Uses `ffmpeg` to generate frames, and [Tesseract](https://github.com/charlesw/tesseract) .NET wrapper for OCR.

### tvDB Integration

**Status:** 💡 Planned

Will allow [tvDB](https://www.thetvdb.com/) integration with valid API key for cross-referencing episode titles
with season and episode numbers for automatic file renaming. Will also work with `Title Scan` for name confidence.
