# Task Manager CLI Guidelines

* [Product Vision and Goals](../docs/PRODUCT.md): Understand the high-level vision and objectives of the Task Manager CLI to ensure alignment with product goals.
* [System Architecture and Design Principles](../docs/ARCHITECTURE.md): Overall system architecture, design patterns, and design principles that guide the development process.
* [Contributing Guidelines](../docs/CONTRIBUTING.md): Overview of the project's contributing guidelines and collaboration practices.

## Project Context

This is a learning lab for GitHub Copilot's context engineering features. The Task Manager CLI serves as a simple, practical example application for demonstrating how to effectively use custom instructions, custom agents, and prompt files to guide AI-assisted development.


## Architectural Patterns

- **Sealed Classes by Default**: Always make classes `sealed` by default unless they are not leaves in the derivation tree (i.e., unless they are intended to be base classes for further inheritance). This prevents unintended inheritance, improves code safety, and clarifies design intent. Only omit `sealed` if the class is explicitly designed to be extended.

## Key Principles

- Keep the implementation simple and focused on core functionality
- Write clear, maintainable code following the style guide
- Ensure all changes are tested and work correctly
- Maintain comprehensive documentation

Suggest to update these documents if you find any incomplete or conflicting information during your work.
