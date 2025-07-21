import { DragDropHandler } from "./modules/dragdrop.js";
import { TaskAPI } from "./modules/api.js";

/**
 * Main Kanban board controller
 */
class KanbanBoard {
  constructor() {
    this.dragDropHandler = new DragDropHandler(this.handleTaskDrop.bind(this));
    this.initializeBoard();
  }

  /**
   * Initialize the Kanban board
   */
  initializeBoard() {
    console.log("Kanban board initialized");
  }

  /**
   * Handle task drop event - update status and move task
   * @param {string} taskId - The task ID
   * @param {string} newStatus - The new status
   * @param {HTMLElement} draggedElement - The dragged DOM element
   */
  async handleTaskDrop(taskId, newStatus, draggedElement) {
    try {
      // Show loading state
      draggedElement.style.opacity = "0.7";
      draggedElement.classList.add("updating");

      // Update task status on server
      await TaskAPI.updateTaskStatus(taskId, newStatus);

      // Move the element to the new column
      this.moveTaskToColumn(draggedElement, newStatus);

      // Show success notification
      TaskAPI.showNotification(`Task moved to ${newStatus}`, "success");

      // Update task counts
      this.updateTaskCounts();
    } catch (error) {
      console.error("Failed to update task:", error);

      // Show error notification
      TaskAPI.showNotification(
        "Failed to move task. Please try again.",
        "error"
      );

      // Reset visual state on error
      draggedElement.style.opacity = "1";
    } finally {
      draggedElement.classList.remove("updating");
    }
  }

  /**
   * Move task element to a new column
   * @param {HTMLElement} taskElement - The task DOM element
   * @param {string} newStatus - The target status/column
   */
  moveTaskToColumn(taskElement, newStatus) {
    const targetColumn = document.querySelector(
      `[data-status="${newStatus}"] .task-container`
    );

    if (targetColumn) {
      taskElement.remove();
      targetColumn.appendChild(taskElement);
      taskElement.style.opacity = "1";
    }
  }

  /**
   * Update task count badges for all columns
   */
  updateTaskCounts() {
    const statuses = ["Backlog", "Ready", "InProgress", "InReview", "Done"];

    statuses.forEach((status) => {
      const column = document.querySelector(`[data-status="${status}"]`);
      if (column) {
        const tasks = column.querySelectorAll('[id^="task-"]');
        const countElement = column.querySelector(".task-count");
        if (countElement) {
          countElement.textContent = tasks.length;
        }
      }
    });
  }

  /**
   * Refresh the board after DOM changes
   */
  refresh() {
    this.dragDropHandler.refresh();
  }
}

// Initialize when DOM is ready
document.addEventListener("DOMContentLoaded", () => {
  window.kanbanBoard = new KanbanBoard();
});
