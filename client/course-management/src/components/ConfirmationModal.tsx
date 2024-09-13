import { FC } from 'react';

interface ConfirmationModalProps {
  onConfirm: () => void;
  onCancel: () => void;
  isOpen: boolean;
}

const ConfirmationModal: FC<ConfirmationModalProps> = ({
  onConfirm,
  onCancel,
  isOpen,
}) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg text-center">
        <h2 className="text-lg font-bold mb-4">Are you sure?</h2>
        <p className="mb-6">Do you really want to delete this course?</p>
        <div className="flex justify-center space-x-4">
          <button
            className="bg-red-600 text-white px-4 py-2 rounded-md"
            onClick={onConfirm}
          >
            Delete
          </button>
          <button
            className="bg-gray-300 text-gray-700 px-4 py-2 rounded-md"
            onClick={onCancel}
          >
            Cancel
          </button>
        </div>
      </div>
    </div>
  );
};

export default ConfirmationModal;
