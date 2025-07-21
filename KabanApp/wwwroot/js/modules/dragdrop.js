/**
 * Handles drag and drop functionality for Kanban board tasks
 */
export class DragDropHandler {
  constructor(onDrop) {
    this.onDrop = onDrop;
    this.draggedElement = null;
    this.initializeDropZones();
  }

  /**
   * Initialize drag and drop zones (Kanban columns)
   */
  initializeDropZones() {
    const dropZones = document.querySelectorAll("[data-status]");

    dropZones.forEach((zone) => {
      zone.addEventListener("dragover", this.handleDragOver.bind(this));
      zone.addEventListener("drop", this.handleDrop.bind(this));
      zone.addEventListener("dragenter", this.handleDragEnter.bind(this));
      zone.addEventListener("dragleave", this.handleDragLeave.bind(this));
    });

    this.initializeDraggableItems();
  }

  /**
   * Initialize draggable task items
   */
  initializeDraggableItems() {
    const draggableItems = document.querySelectorAll('[draggable="true"]');

    draggableItems.forEach((item) => {
      item.addEventListener("dragstart", this.handleDragStart.bind(this));
      item.addEventListener("dragend", this.handleDragEnd.bind(this));
    });
  }

  /**
   * Handle drag start event - store task data and apply visual feedback
   */
  handleDragStart(event) {
    this.draggedElement = event.target;
    const taskId = event.target.id.replace("task-", "");

    event.dataTransfer.setData("text/plain", taskId);
    event.dataTransfer.effectAllowed = "move";

    // Apply visual feedback
    event.target.style.opacity = "0.5";
    event.target.classList.add("dragging");
  }

  /**
   * Handle drag end event - reset visual feedback
   */
  handleDragEnd(event) {
    event.target.style.opacity = "1";
    event.target.classList.remove("dragging");
    this.draggedElement = null;

    // Clean up any remaining drop zone highlights
    document.querySelectorAll(".drop-zone-highlight").forEach((zone) => {
      zone.classList.remove("drop-zone-highlight");
    });
  }

  /**
   * Handle drag over event - allow drop
   */
  handleDragOver(event) {
    event.preventDefault();
    event.dataTransfer.dropEffect = "move";
  }

  /**
   * Handle drag enter event - highlight drop zone
   */
  handleDragEnter(event) {
    event.preventDefault();
    const dropZone = event.target.closest("[data-status]");
    if (dropZone) {
      dropZone.classList.add("drop-zone-highlight");
    }
  }

  /**
   * Handle drag leave event - remove highlight when leaving drop zone
   */
  handleDragLeave(event) {
    const dropZone = event.target.closest("[data-status]");
    if (dropZone && !dropZone.contains(event.relatedTarget)) {
      dropZone.classList.remove("drop-zone-highlight");
    }
  }

  /**
   * Handle drop event - execute the drop callback
   */
  handleDrop(event) {
    event.preventDefault();

    const taskId = event.dataTransfer.getData("text/plain");
    const dropZone = event.target.closest("[data-status]");

    if (dropZone) {
      const status = dropZone.getAttribute("data-status");
      dropZone.classList.remove("drop-zone-highlight");

      if (this.onDrop) {
        this.onDrop(taskId, status, this.draggedElement);
      }
    }
  }

  /**
   * Refresh draggable items after DOM changes
   */
  refresh() {
    this.initializeDraggableItems();
  }
}
