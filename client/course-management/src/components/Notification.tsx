import { FC } from 'react';

interface NotificationProps {
  messages: string[];
  onClose: () => void;
}

const Notification: FC<NotificationProps> = ({ messages, onClose }) => {
  if (messages.length === 0) return null;

  return (
    <div className="fixed top-0 left-1/2 transform -translate-x-1/2 mt-4 bg-red-500 text-white p-4 rounded-md shadow-lg max-w-lg w-full z-50">
      <div className="flex justify-between">
        <p className="font-bold">Error</p>
        <button onClick={onClose} className="text-white">
          &times;
        </button>
      </div>
      <ul className="mt-2">
        {messages.map((message, index) => (
          <li key={index}>{message}</li>
        ))}
      </ul>
    </div>
  );
};

export default Notification;
