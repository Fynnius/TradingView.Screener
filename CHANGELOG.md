# Changelog

All notable changes to this project will be documented in this file.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project uses semantic versioning.

## [Unreleased]

### Changed
- Removed preview package dependencies that are already provided by the target framework.
- Simplified GitHub Actions workflows for CI and NuGet publishing.

## [1.0.0] - 2025-02-01

### Added
- Initial `TradingView.Screener` NuGet package.
- Initial `TradingView.Screener.Cli` global tool package.
- Fluent query API for TradingView scanner requests.
- CLI support for basic market, price, volume, market cap, exchange, sector, and industry filters.
- Unit tests for query construction and column filter operations.

[Unreleased]: https://github.com/Fynnius/TradingView.Screener/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/Fynnius/TradingView.Screener/releases/tag/v1.0.0
