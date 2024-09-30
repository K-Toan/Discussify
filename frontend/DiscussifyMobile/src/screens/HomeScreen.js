import React from 'react';
import { View, SafeAreaView, Text, FlatList, Image, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { theme } from '../components/theme';

const posts = [
    {
        id: 1,
        avatar: 'https://picsum.photos/200',
        userType: 'u/',
        userName: 'JohnDoe',
        timePosted: '2h ago',
        title: 'Làm sao để đánh được Garen đi top?',
        content: 'City on fire when I coming home...',
        image: 'https://picsum.photos/200/300/?blur',
        upvotes: 12,
        downvotes: 45,
        comments: 20,
    },
    {
        id: 2,
        avatar: 'https://picsum.photos/120',
        userType: 'r/',
        userName: 'LeagueCommunity',
        timePosted: '5h ago',
        title: 'Cách chọn trang bị cho Garen',
        content: 'City on fire when I coming home...',
        upvotes: 324,
        downvotes: 21,
        comments: 55,
    },
    {
        id: 3,
        avatar: 'https://picsum.photos/210',
        userType: 'u/',
        userName: 'JaneSmith',
        timePosted: '1d ago',
        title: 'Hướng dẫn chơi Yasuo cho người mới bắt đầu',
        content: 'Yasuo là một tướng rất khó nhưng vô cùng thú vị nếu bạn chơi đúng cách...',
        image: 'https://picsum.photos/210/310',
        upvotes: 150,
        downvotes: 10,
        comments: 40,
    },
    {
        id: 4,
        avatar: 'https://picsum.photos/130',
        userType: 'u/',
        userName: 'GameLover',
        timePosted: '3d ago',
        title: 'Những tướng mạnh trong meta hiện tại',
        content: 'Trong meta hiện tại, các tướng như Darius, Kai’sa, và Sylas đang có tỉ lệ thắng cao...',
        upvotes: 500,
        downvotes: 25,
        comments: 120,
    },
    {
        id: 5,
        avatar: 'https://picsum.photos/140',
        userType: 'r/',
        userName: 'MOBAForum',
        timePosted: '1w ago',
        title: 'Tổng hợp các trang bị hiệu quả nhất cho hỗ trợ',
        content: 'Hỗ trợ là một vị trí quan trọng và những trang bị như Locket of the Iron Solari...',
        image: 'https://picsum.photos/140/240',
        upvotes: 230,
        downvotes: 18,
        comments: 75,
    },
];

export default function HomeScreen() {
    return (
        <View style={{ paddingLeft: 10, paddingRight: 10, backgroundColor: theme.background }}>
            <FlatList
                data={posts}
                keyExtractor={(item) => item.id.toString()}
                renderItem={({ item }) => (
                    <View style={{ padding: 0, paddingBottom: 10, borderBottomWidth: 1, borderColor: theme.borderColor, marginBottom: 10 }}>
                        {/* Row 1: Avatar, User Type (u/ or r/), Time */}
                        <View style={{ flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between', marginBottom: 8 }}>
                            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                                <Image
                                    source={{ uri: item.avatar }}
                                    style={{ width: 36, height: 36, borderRadius: 20, marginRight: 10 }}
                                />
                                <View style={{ flexDirection: 'column', alignItems: 'flex-start' }}>
                                    <Text style={{ fontWeight: 'bold', color: theme.text }}>
                                        {item.userType}{item.userName}
                                    </Text>
                                    <Text style={{ marginLeft: 0, fontSize: 12, color: 'gray' }}>{item.timePosted}</Text>
                                </View>
                            </View>
                        </View>

                        {/* Row 2: Post Title */}
                        <Text style={{ fontWeight: 'bold', fontSize: 16, marginBottom: 0, color: theme.text }}>
                            {item.title}
                        </Text>

                        {/* Row 3: Post Content */}
                        <Text style={{ marginBottom: 0, color: theme.subtext }}>
                            {item.content}
                        </Text>

                        {/* Optional Image/Video */}
                        {item.image && (
                            <Image
                                source={{ uri: item.image }}
                                style={{ width: '100%', height: 200, marginTop: 10, borderRadius: 10 }}
                                resizeMode="cover"
                            />
                        )}

                        {/* Row 4: Upvote, Downvote, Comments, Share, and 3-dots Icon */}
                        <View style={{ flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between', marginTop: 10 }}>
                            {/* Left Group: Upvote, Downvote, Comments */}
                            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                                {/* Upvote */}
                                <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center', marginRight: 15 }}>
                                    <Ionicons name="arrow-up-outline" size={20} color={theme.iconColor} />
                                    <Text style={{ marginLeft: 2, color: theme.text, fontSize: 12 }}>{item.upvotes}</Text>
                                </TouchableOpacity>

                                {/* Downvote */}
                                <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center', marginRight: 15 }}>
                                    <Ionicons name="arrow-down-outline" size={20} color={theme.iconColor} />
                                    <Text style={{ marginLeft: 2, color: theme.text, fontSize: 12 }}>{item.downvotes}</Text>
                                </TouchableOpacity>

                                {/* Comments */}
                                <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center', marginRight: 15 }}>
                                    <Ionicons name="chatbubble-outline" size={20} color={theme.iconColor} />
                                    <Text style={{ marginLeft: 4, color: theme.text, fontSize: 12 }}>{item.comments}</Text>
                                </TouchableOpacity>
                            </View>

                            {/* Right Group: Share and 3-dots Menu */}
                            <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                                {/* Share */}
                                <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center', marginRight: 15 }}>
                                    <Ionicons name="share-outline" size={20} color={theme.iconColor} />
                                    <Text style={{ marginLeft: 4, color: theme.text }}>{/*Share*/}</Text>
                                </TouchableOpacity>

                                {/* 3-dots menu */}
                                <TouchableOpacity style={{ flexDirection: 'row', alignItems: 'center' }}>
                                    <Ionicons name="ellipsis-horizontal" size={20} color={theme.iconColor} />
                                </TouchableOpacity>
                            </View>
                        </View>
                    </View>
                )}
            />
        </View>
    );
}
