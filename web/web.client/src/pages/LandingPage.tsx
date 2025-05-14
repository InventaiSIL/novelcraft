import React from 'react';
import { Link } from 'react-router-dom';

const LandingPage: React.FC = () => {
  return (
    <div className="min-h-screen h-full bg-gradient-to-br from-slate-900 to-slate-800 text-white flex flex-col w-full">
      {/* Hero Section */}
      <section className="flex-1 relative overflow-hidden pt-20 pb-20 md:pt-28 md:pb-28">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 relative z-10">
          <div className="grid md:grid-cols-2 gap-12 items-center">
            <div className="space-y-8">
              <h1 className="text-5xl md:text-6xl lg:text-7xl font-bold leading-tight">
                <span className="block">Create Interactive</span>
                <span className="block bg-clip-text text-transparent bg-gradient-to-r from-blue-400 to-purple-500">
                  Choice-Based Games
                </span>
              </h1>
              <p className="text-xl text-gray-300 max-w-lg">
                Transform your ideas into engaging interactive experiences with our AI-powered game creator.
              </p>
              <div className="flex flex-col sm:flex-row gap-4 pt-4">
                <Link
                  to="/novel-generator"
                  className="px-8 py-4 bg-gradient-to-r from-blue-500 to-purple-600 rounded-lg font-medium text-lg hover:shadow-lg transition-all duration-300"
                >
                  Start Creating
                </Link>
              </div>
            </div>
            <div className="relative">
              <div className="relative bg-slate-800/50 backdrop-blur-lg border border-gray-700 rounded-2xl overflow-hidden shadow-2xl">
                <div className="p-6">
                  <div className="text-gray-400 mb-4">AI Game Creator</div>
                  <div className="space-y-4">
                    <div className="h-4 bg-gray-700 rounded w-3/4"></div>
                    <div className="h-4 bg-gray-700 rounded"></div>
                  </div>
                  <div className="mt-8 pt-4 border-t border-gray-700 text-sm text-gray-500">
                    Generating your interactive story...
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section className="py-20">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center max-w-3xl mx-auto mb-16">
            <h2 className="text-4xl md:text-5xl font-bold mb-6">
              Everything you need to create immersive experiences
            </h2>
            <p className="text-xl text-gray-400">
              Our platform combines cutting-edge AI with intuitive tools to help you craft engaging interactive stories.
            </p>
          </div>
        </div>
      </section>

      {/* Footer */}
      <footer className="bg-slate-900 border-t border-gray-800">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
          <p className="text-gray-400 text-sm">
            &copy; {new Date().getFullYear()} InventAI. All rights reserved.
          </p>
        </div>
      </footer>
    </div>
  );
};

export default LandingPage;