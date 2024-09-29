import React from 'react';
import HomeScreen from '../screens/HomeScreen';
import CreateScreen from '../screens/CreateScreen';
import ProfileScreen from '../screens/ProfileScreen';
import CommunitiesScreen from '../screens/CommunitiesScreen';
import NotificationsScreen from '../screens/NotificationsScreen';
import { Ionicons } from '@expo/vector-icons';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { theme } from '../components/theme';

const Tab = createBottomTabNavigator();

export default function BottomTabComponent() {
  return (
    <Tab.Navigator style={{}}
      screenOptions={({ route }) => ({
        tabBarIcon: ({ color, size }) => {
          let iconName;
          if (route.name === 'Home') {
            iconName = 'home';
          }
          else if (route.name === 'Communities') {
            iconName = 'people';
          }
          else if (route.name === 'Create') {
            iconName = 'add-circle';
          }
          else if (route.name === 'Notifications') {
            iconName = 'notifications';
          }
          else if (route.name === 'Profile') {
            iconName = 'person';
          }
          return <Ionicons name={iconName} size={size} color={color} />;
        },
        tabBarActiveTintColor: theme.iconActiveColor,
        tabBarInactiveTintColor: theme.iconColor,
        tabBarStyle: { backgroundColor: theme.background, borderTopColor: theme.background, paddingBottom: 4 },
        headerShown: false,
      })}
    >
      <Tab.Screen name="Home" component={HomeScreen} />
      <Tab.Screen name="Communities" component={CommunitiesScreen} />
      <Tab.Screen name="Create" component={CreateScreen} />
      <Tab.Screen name="Notifications" component={NotificationsScreen} />
      <Tab.Screen name="Profile" component={ProfileScreen} />
    </Tab.Navigator>
  );
}
