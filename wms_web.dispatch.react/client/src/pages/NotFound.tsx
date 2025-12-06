import React from 'react';
import { useNavigate } from 'react-router-dom';
import { 
  Construction, 
  ArrowLeft, 
  Home, 
  Code, 
  Clock,
  AlertTriangle 
} from 'lucide-react';
import { Button, Card } from '../components/ui';

const NotFound: React.FC = () => {
  const navigate = useNavigate();

  const handleGoBack = () => {
    navigate(-1);
  };

  const handleGoHome = () => {
    navigate('/');
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-50 via-white to-gray-100 dark:from-gray-900 dark:via-gray-800 dark:to-gray-900 flex items-center justify-center p-4">
      {/* Background Pattern */}
      <div className="absolute inset-0 bg-[url('data:image/svg+xml,%3Csvg%20width%3D%2260%22%20height%3D%2260%22%20viewBox%3D%220%200%2060%2060%22%20xmlns%3D%22http%3A//www.w3.org/2000/svg%22%3E%3Cg%20fill%3D%22none%22%20fill-rule%3D%22evenodd%22%3E%3Cg%20fill%3D%22%2523f3f4f6%22%20fill-opacity%3D%220.1%22%3E%3Ccircle%20cx%3D%2230%22%20cy%3D%2230%22%20r%3D%222%22/%3E%3C/g%3E%3C/g%3E%3C/svg%3E')] opacity-40"></div>
      
      <div className="relative max-w-2xl mx-auto text-center">
        <Card className="shadow-2xl border-0 bg-white/80 backdrop-blur-sm dark:bg-gray-800/80">
          <div className="p-12">
            {/* Icon */}
            <div className="flex justify-center mb-8">
              <div className="relative">
                <div className="w-24 h-24 bg-gradient-to-br from-warning-100 to-warning-200 dark:from-warning-500/20 dark:to-warning-600/20 rounded-full flex items-center justify-center">
                  <Construction className="w-12 h-12 text-warning-600 dark:text-warning-400" />
                </div>
                {/* Animated dots */}
                <div className="absolute -top-2 -right-2">
                  <div className="flex space-x-1">
                    <div className="w-2 h-2 bg-warning-500 rounded-full animate-bounce" style={{ animationDelay: '0ms' }}></div>
                    <div className="w-2 h-2 bg-warning-500 rounded-full animate-bounce" style={{ animationDelay: '150ms' }}></div>
                    <div className="w-2 h-2 bg-warning-500 rounded-full animate-bounce" style={{ animationDelay: '300ms' }}></div>
                  </div>
                </div>
              </div>
            </div>

            {/* Main Content */}
            <div className="mb-8">
              <h1 className="text-4xl font-bold text-gray-900 dark:text-white mb-4">
                Page Under Development
              </h1>
              <div className="flex items-center justify-center gap-2 mb-4">
                <Code className="w-5 h-5 text-primary-600 dark:text-primary-400" />
                <span className="text-lg font-medium text-primary-600 dark:text-primary-400">
                  404 - Coming Soon!
                </span>
              </div>
              <p className="text-gray-600 dark:text-gray-400 text-lg leading-relaxed mb-6">
                The page you're looking for is currently being built by our development team. 
                We're working hard to bring you new features and improvements.
              </p>
            </div>

            {/* Status Cards */}
            <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
              <div className="bg-blue-50 dark:bg-blue-500/20 p-4 rounded-lg">
                <div className="flex items-center justify-center mb-2">
                  <Clock className="w-6 h-6 text-blue-600 dark:text-blue-400" />
                </div>
                <div className="text-sm font-medium text-blue-600 dark:text-blue-400 mb-1">Status</div>
                <div className="text-blue-900 dark:text-blue-300 font-semibold">In Development</div>
              </div>
              
              <div className="bg-green-50 dark:bg-green-500/20 p-4 rounded-lg">
                <div className="flex items-center justify-center mb-2">
                  <Construction className="w-6 h-6 text-green-600 dark:text-green-400" />
                </div>
                <div className="text-sm font-medium text-green-600 dark:text-green-400 mb-1">Progress</div>
                <div className="text-green-900 dark:text-green-300 font-semibold">Building...</div>
              </div>
              
              <div className="bg-purple-50 dark:bg-purple-500/20 p-4 rounded-lg">
                <div className="flex items-center justify-center mb-2">
                  <AlertTriangle className="w-6 h-6 text-purple-600 dark:text-purple-400" />
                </div>
                <div className="text-sm font-medium text-purple-600 dark:text-purple-400 mb-1">Expected</div>
                <div className="text-purple-900 dark:text-purple-300 font-semibold">Soon</div>
              </div>
            </div>

            {/* What you can do */}
            <div className="bg-gray-50 dark:bg-gray-800 p-6 rounded-lg mb-8">
              <h3 className="text-lg font-semibold text-gray-900 dark:text-white mb-3">
                What you can do:
              </h3>
              <ul className="text-left text-gray-600 dark:text-gray-400 space-y-2">
                <li className="flex items-center gap-2">
                  <div className="w-2 h-2 bg-primary-500 rounded-full"></div>
                  Go back to the previous page
                </li>
                <li className="flex items-center gap-2">
                  <div className="w-2 h-2 bg-primary-500 rounded-full"></div>
                  Return to the dashboard
                </li>
                <li className="flex items-center gap-2">
                  <div className="w-2 h-2 bg-primary-500 rounded-full"></div>
                  Check back later for updates
                </li>
                <li className="flex items-center gap-2">
                  <div className="w-2 h-2 bg-primary-500 rounded-full"></div>
                  Contact support if you need immediate assistance
                </li>
              </ul>
            </div>

            {/* Action Buttons */}
            <div className="flex flex-col sm:flex-row gap-4 justify-center">
              <Button
                variant="outline"
                onClick={handleGoBack}
                className="flex items-center gap-2"
              >
                <ArrowLeft className="w-4 h-4" />
                Go Back
              </Button>
              <Button
                variant="primary"
                onClick={handleGoHome}
                className="flex items-center gap-2"
              >
                <Home className="w-4 h-4" />
                Go to Dashboard
              </Button>
            </div>

            {/* Footer Note */}
            <div className="mt-8 pt-6 border-t border-gray-200 dark:border-gray-700">
              <p className="text-sm text-gray-500 dark:text-gray-400">
                If you believe this is an error, please contact the system administrator.
              </p>
            </div>
          </div>
        </Card>
      </div>
    </div>
  );
};

export default NotFound;