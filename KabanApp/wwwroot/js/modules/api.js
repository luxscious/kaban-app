/**
 * API utilities for task management
 */
export class TaskAPI {
  /**
   * Update task status via AJAX
   * @param {string} taskId - The task ID
   * @param {string} status - The new status
   * @returns {Promise} API response
   */
  static async updateTaskStatus(taskId, status) {
    try {
      const token = document.querySelector(
        '[name="__RequestVerificationToken"]'
      )?.value;

      const response = await fetch("/Tasks/UpdateStatus", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          ...(token && { RequestVerificationToken: token }),
        },
        body: JSON.stringify({
          taskId: parseInt(taskId),
          status: status,
        }),
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      return await response.json();
    } catch (error) {
      console.error("Error updating task status:", error);
      throw error;
    }
  }

  /**
   * Create a new task
   * @param {Object} taskData - Task data object
   * @returns {Promise} API response
   */
  static async createTask(taskData) {
    try {
      const token = document.querySelector(
        '[name="__RequestVerificationToken"]'
      )?.value;

      const response = await fetch("/Tasks/Create", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          ...(token && { RequestVerificationToken: token }),
        },
        body: JSON.stringify(taskData),
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      return await response.json();
    } catch (error) {
      console.error("Error creating task:", error);
      throw error;
    }
  }

  /**
   * Display notification to user
   * @param {string} message - Message to display
   * @param {string} type - Notification type (info, success, error)
   */
  static showNotification(message, type = "info") {
    const notification = document.createElement("div");
    notification.className = `notification notification-${type} fixed top-4 right-4 p-4 rounded shadow-lg z-50`;

    const colors = {
      error: "#ef4444",
      success: "#10b981",
      info: "#3b82f6",
    };

    notification.style.cssText = `
      background: ${colors[type] || colors.info};
      color: white;
      transition: all 0.3s ease;
    `;
    notification.textContent = message;

    document.body.appendChild(notification);

    // Auto-remove after 3 seconds
    setTimeout(() => {
      notification.style.opacity = "0";
      setTimeout(() => notification.remove(), 300);
    }, 3000);
  }
}
